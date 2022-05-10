namespace MyToDo.MVVM.ViewModels;

public class IndexViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }

    public IScreen HostScreen { get; }
    public ObservableCollection<TaskBar> TaskBars { get; set; } = new();
    public ObservableCollection<MemoDto> MemoDtos { get; set; } = new();
    public ObservableCollection<ToDoDto> ToDoDtos { get; set; } = new();
    public ReactiveCommand<TaskBar, IObservable<IRoutableViewModel>?> NavigateCommand { get; set; }

    public IndexViewModel()
    {
        HostScreen = Current.GetService<IScreen>("MainContent");
    }

    protected override void SetupStart()
    {
        base.SetupStart();
        CreatTaskBars();
        CreatTestData();
    }

    private void CreatTestData()
    {
        for (int i = 0; i < 5; i++)
        {
            ToDoDtos.Add(new ToDoDto { Content = "代办" + i, Title = "代办" + 1 });
            MemoDtos.Add(new MemoDto { Content = "备忘" + i, Title = "备忘" + i });
        }
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