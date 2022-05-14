namespace MyToDo.MVVM.ViewModels;

public class SettingViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; } = new();
    public ObservableCollection<MenuBar> MenubarItems { get; set; } = new();
    [Reactive] public MenuBar? MenuBarSelectItem { get; set; }

    protected override void SetupSubscriptions(CompositeDisposable d)
    {
        base.SetupSubscriptions(d);
        this.WhenAnyValue(x => x.MenuBarSelectItem)
            .WhereNotNull()
            .Select(item => Current.GetService<IRoutableViewModel>(item.ViewName))
            .WhereNotNull()
            .Subscribe(vm => Router.Navigate.Execute(vm));
    }

    protected override void SetupStart()
    {
        base.SetupStart();
        CreatMenuItems();
    }

    private void CreatMenuItems()
    {
        MenubarItems.Add(new MenuBar() { Icon = "Palette", Title = "个性化", ViewName = ViewName.SkinName });
        MenubarItems.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", ViewName = "" });
        MenubarItems.Add(new MenuBar() { Icon = "Information", Title = "关于", ViewName = ViewName.AboutName });
    }
}