namespace MyToDo.MVVM.ViewModels;

public class SkinViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    public SkinViewModel()
    {
        HostScreen = Current.GetService<IScreen>("SettingContent");
    }
}