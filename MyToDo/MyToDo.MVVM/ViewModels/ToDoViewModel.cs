namespace MyToDo.MVVM.ViewModels;

public class ToDoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}