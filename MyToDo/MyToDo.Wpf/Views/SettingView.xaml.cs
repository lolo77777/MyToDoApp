namespace MyToDo.Wpf.Views;

/// <summary>
/// SettingView.xaml 的交互逻辑
/// </summary>
public partial class SettingView
{
    public SettingView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, vm => vm.MenubarItems, v => v.menuItems.ItemsSource).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.Router, v => v.routedViewHostSetting.Router).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.MenuBarSelectItem, v => v.menuItems.SelectedItem).DisposeWith(d);
        });
    }
}