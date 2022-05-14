namespace MyToDo.MVVM.ViewModels;

public class MemoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public ObservableCollection<ToDoDto> MemoDtos { get; private set; } = new();
    [Reactive] public bool IsDrawerHostAddToDoIsOpen { get; set; }
    public ReactiveCommand<Unit, bool> AddToDoCommand { get; set; }

    protected override void SetupStart()
    {
        base.SetupStart();
        CreatTestData();
    }

    protected override void SetupCommands()
    {
        base.SetupCommands();
        AddToDoCommand = ReactiveCommand.Create(() => IsDrawerHostAddToDoIsOpen = true);
    }

    private void CreatTestData()
    {
        for (int i = 0; i < 50; i++)
        {
            MemoDtos.Add(new ToDoDto { Title = "Memo" + i, Content = "待处理中。。。" });
        }
    }
}