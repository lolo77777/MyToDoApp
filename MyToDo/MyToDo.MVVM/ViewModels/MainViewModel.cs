using System.Runtime.Serialization;

namespace MyToDo.MVVM.ViewModels;

[DataContract]
public class MainViewModel : ViewModelBase, IScreen
{
    public ObservableCollection<MenuBar> MenubarItems { get; set; } = new();
    [Reactive] public RoutingState Router { get; set; } = new();

    [DataMember]
    [Reactive] public MenuBar? MenuBarSelectItem { get; set; }

    public ICommand? NaviBackCommand { get; set; }
    public ICommand? NaviForwardCommand { get; set; }
    public string Greeting { get; set; } = "hello avalonia";

    public MainViewModel()
    {
    }

    protected override void SetupSubscriptions(CompositeDisposable d)
    {
        base.SetupSubscriptions(d);
        this.WhenAnyValue(x => x.MenuBarSelectItem)
            .WhereNotNull()
            .Select(item => Current.GetService<IRoutableViewModel>(item.ViewName))
            .WhereNotNull()
            .Do(vm => Router.Navigate.Execute(vm))
            .Subscribe(vm => GC.Collect());
    }

    protected override void SetupStart()
    {
        base.SetupStart();
        CreatMenuItems();
    }

    protected override void SetupCommands()
    {
        base.SetupCommands();
        var canNaviBack = this.WhenAnyValue(x => x.Router.NavigationStack.Count)
            .Select(c => c > 1);

        NaviBackCommand = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default), canNaviBack);
    }

    private void CreatMenuItems()
    {
        MenubarItems.Add(new MenuBar() { Icon = "Home", Title = "首页", ViewName = ViewName.IndexName });
        MenubarItems.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", ViewName = ViewName.ToDoName });
        MenubarItems.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", ViewName = ViewName.MemoName });
        MenubarItems.Add(new MenuBar() { Icon = "Cog", Title = "设置", ViewName = ViewName.SettingName });

        //MenuBarSelectItem = MenubarItems[0];
    }
}