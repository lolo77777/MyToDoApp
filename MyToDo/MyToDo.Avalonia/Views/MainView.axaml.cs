namespace MyToDo.Avalonia.Views;

public partial class MainView : ReactiveWindow<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        ViewModel = (MainViewModel?)Current.GetService<IScreen>("MainContent");

        this.WhenActivated(d =>
        {
            var movingOb = title.Events().PointerMoved.Select(e => e.Pointer.IsPrimary);
            title.Events().PointerPressed.CombineLatest(movingOb).Where(vt => vt.Second).Subscribe(vt => BeginMoveDrag(vt.First)).DisposeWith(d);
            menuItems.SelectionChanged += (s, e) => naviDrawer.LeftDrawerOpened = false;
            this.OneWayBind(ViewModel, vm => vm.MenubarItems, v => v.menuItems.Items).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.Router, v => v.mainContent.Router).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.MenuBarSelectItem, v => v.menuItems.SelectedItem).DisposeWith(d);
            this.BindCommand(ViewModel, vm => vm.NaviBackCommand, v => v.btnNaviBack).DisposeWith(d);
        });
    }

    private void OnButtonMinClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void OnButtonMaxClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void OnButtonCloseClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}