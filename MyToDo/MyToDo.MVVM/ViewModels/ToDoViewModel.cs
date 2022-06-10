namespace MyToDo.MVVM.ViewModels;

public class ToDoViewModel : ViewModelBase, IRoutableViewModel
{
    private IToDoApi _todoApi;
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    [Reactive] public ObservableCollection<ToDoDto> ToDoDtos { get; set; } = new();
    [Reactive] public string SearchStr { get; set; }
    [Reactive] public bool IsDrawerHostAddToDoIsOpen { get; set; }
    [Reactive] public int Status { get; set; }
    [Reactive] public string ToDoDtoTitle { get; set; }
    [Reactive] public string ToDoDtoContent { get; set; }
    [Reactive] public int ToDoDtoStatus { get; set; }
    [ObservableAsProperty] public bool IsAddingToDoDto { get; set; }

    public ReactiveCommand<Unit, bool> AddToDoCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddToDoDtoCommand { get; set; }

    public ToDoViewModel(IToDoApi toDoApi)
    {
        _todoApi = toDoApi;
    }

    protected override void SetupStart()
    {
        base.SetupStart();
    }

    protected override void SetupCommands()
    {
        base.SetupCommands();
        AddToDoCommand = ReactiveCommand.Create(() => IsDrawerHostAddToDoIsOpen = true);
        AddToDoDtoCommand = ReactiveCommand.CreateFromTask(AddToDoDto);
    }

    protected override void SetupSubscriptions(CompositeDisposable d)
    {
        base.SetupSubscriptions(d);
        this.WhenAnyValue(x => x.SearchStr, x => x.Status)
            .Throttle(TimeSpan.FromMilliseconds(300))
            .WhereNotNull()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(x => UpdateToDos())
            .DisposeWith(d);
        AddToDoCommand.IsExecuting
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToPropertyEx(this, x => x.IsAddingToDoDto)
            .DisposeWith(d);
    }

    private async Task AddToDoDto()
    {
        ToDoDto toDoDto = new() { Title = ToDoDtoTitle, Content = ToDoDtoContent, Status = ToDoDtoStatus };
        var ret = await _todoApi.AddTodo(toDoDto);
        if (ret.IsSuccessStatusCode)
        {
            await Task.Delay(5000);
        }
    }

    private async void UpdateToDos()
    {
        ToDoDtos.Clear();
        var ret = await _todoApi.GetTodos(new ToDoParameter { PageIndex = 0, PageSize = 100, Search = SearchStr, Status = Status });
        if (ret.IsSuccessStatusCode && ret.Content != null && ret.Content.Any())
        {
            ToDoDtos = new ObservableCollection<ToDoDto>(ret.Content);
        }
    }
}