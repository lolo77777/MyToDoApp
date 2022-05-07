namespace MyToDo.MVVM.ViewModels;

public class MemoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}