using MyToDo.MVVM.Common.Drivers;

namespace MyToDo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly AutoSuspendHelper autoSuspendHelper;

    public App()
    {
        this.autoSuspendHelper = new AutoSuspendHelper(this);
        RxApp.SuspensionHost.CreateNewAppState = () => new MainViewModel();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<MainViewModel>());

        _ = new BootStarpper();
        ConfigRegister.RegisterViewForViewModel();
    }
}