namespace MyToDo.MVVM.ViewModels;

public class SettingViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}