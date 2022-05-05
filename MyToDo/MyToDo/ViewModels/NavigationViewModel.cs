namespace MyToDo.ViewModels;

public class NavigationViewModel : ViewModelBase
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}