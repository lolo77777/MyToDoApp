namespace MyToDo.Wpf.Views;

/// <summary>
/// MainView.xaml 的交互逻辑
/// </summary>
public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
        ViewModel = Current.GetService<MainViewModel>();
        this.WhenActivated(d =>
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            btnClose.Click += (s, e) => Close();
            btnMin.Click += (s, e) => WindowState = WindowState.Minimized;
            btnMax.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            titleBar.MouseDoubleClick += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            menuItems.SelectionChanged += (s, e) => drawHost.IsLeftDrawerOpen = false;
            this.Events().MouseMove
                .Where(e => e.LeftButton == MouseButtonState.Pressed)
                .Subscribe(_ => DragMove());

            this.OneWayBind(ViewModel, vm => vm.MenubarItems, v => v.menuItems.ItemsSource).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.Router, v => v.mainContent.Router).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.MenuBarSelectItem, v => v.menuItems.SelectedItem).DisposeWith(d);
            this.BindCommand(ViewModel, vm => vm.NaviBackCommand, v => v.btnNaviBack).DisposeWith(d);
        });
    }
}