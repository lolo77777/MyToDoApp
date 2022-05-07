namespace MyToDo.MVVM.ViewModels;

public class NavigationViewModel : ViewModelBase
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}