using System.Reactive;

namespace MyToDo.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    [Reactive] public string TestStr { get; set; } = "hello";

    public ObservableCollection<MenuBar> MenubarItems { get; set; } = new();
    public RoutingState Router { get; } = new();
    [Reactive] public MenuBar? MenuBarSelectItem { get; set; }
    public ICommand NaviBackCommand { get; set; }
    public ICommand NaviForwardCommand { get; set; }

    public MainViewModel()
    {
    }

    protected override void SetupSubscriptions(CompositeDisposable d)
    {
        base.SetupSubscriptions(d);
        this.WhenAnyValue(x => x.MenuBarSelectItem)
            .WhereNotNull()
            .Select(item => Current.GetService<IRoutableViewModel>(item.ViewModelName))
            .WhereNotNull()
            .Do(_ =>
            {
                var t = Router.NavigationStack;
            })
            .Subscribe(vm => Router.Navigate.Execute(vm));
        //this.WhenAnyValue(x => x.Router.NavigationStack.Count)
        //    .Do((c =>
        //    {
        //    }))
        //    .Where(c => c > 0)
        //    .Select(c => Router.NavigationStack.ElementAt(c - 1))
        //    .Where(cVm => cVm.GetType().Name != MenuBarSelectItem?.ViewModelName)
        //    .Subscribe(cVm =>
        //    {
        //        MenuBarSelectItem = MenubarItems.First(it => it.ViewModelName == cVm.GetType().Name);
        //    });
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
        MenubarItems.Add(new MenuBar() { Icon = "Home", Title = "首页", ViewModelName = nameof(IndexViewModel) });
        MenubarItems.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", ViewModelName = nameof(ToDoViewModel) });
        MenubarItems.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", ViewModelName = nameof(MemoViewModel) });
        MenubarItems.Add(new MenuBar() { Icon = "Cog", Title = "设置", ViewModelName = nameof(SettingViewModel) });

        MenuBarSelectItem = MenubarItems.First();
    }
}