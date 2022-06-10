using MyToDo.MVVM.Services;

namespace MyToDo.MVVM;

public class BootStarpper
{
    public BootStarpper()
    {
        ConfigerRegister();
    }

    private static void ConfigerRegister()
    {
        SplatRegistrations.SetupIOC();
        string ApiUrl = "http://localhost:5063/api";
        SplatRegistrations.RegisterConstant<IToDoApi>(RestService.For<IToDoApi>(ApiUrl));
        SplatRegistrations.RegisterConstant<IMemoApi>(RestService.For<IMemoApi>(ApiUrl));
        SplatRegistrations.RegisterConstant<ILoginApi>(RestService.For<ILoginApi>(ApiUrl));
        SplatRegistrations.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainViewModel>(), "MainContent");
        SettingViewModel settingVM = new();
        SplatRegistrations.RegisterConstant<IScreen>(settingVM, "SettingContent");
        SplatRegistrations.RegisterConstant<IRoutableViewModel>(settingVM, ViewName.SettingName);
        SplatRegistrations.Register<IRoutableViewModel, IndexViewModel>(ViewName.IndexName);
        SplatRegistrations.Register<IRoutableViewModel, MemoViewModel>(ViewName.MemoName);
        SplatRegistrations.Register<IRoutableViewModel, ToDoViewModel>(ViewName.ToDoName);
        SplatRegistrations.Register<IRoutableViewModel, SkinViewModel>(ViewName.SkinName);
        SplatRegistrations.Register<IRoutableViewModel, AboutViewModel>(ViewName.AboutName);
    }
}