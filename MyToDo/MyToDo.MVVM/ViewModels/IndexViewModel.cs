namespace MyToDo.MVVM.ViewModels;

public class IndexViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }

    public IScreen HostScreen { get; }
    public ObservableCollection<TaskBar> TaskBars { get; set; } = new();

    public ReactiveCommand<TaskBar, IObservable<IRoutableViewModel>?> NavigateCommand { get; set; }

    public IndexViewModel()
    {
        HostScreen = Current.GetService<IScreen>("MainContent");
    }

    protected override void SetupStart()
    {
        base.SetupStart();
        CreatTaskBars();
    }

    protected override void SetupCommands()
    {
        base.SetupCommands();
        NavigateCommand = ReactiveCommand.Create<TaskBar, IObservable<IRoutableViewModel>?>(taskBar => string.IsNullOrEmpty(taskBar.Target) ? null : HostScreen.Router.Navigate.Execute(Current.GetService<IRoutableViewModel>(taskBar.Target)));
    }

    private void CreatTaskBars()
    {
        TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = ViewName.ToDoName });
        TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = ViewName.ToDoName });
        TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "" });
        TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = ViewName.MemoName });
    }
}