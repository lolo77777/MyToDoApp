using MyToDo.MVVM.Common.Drivers;

using System.Net.NetworkInformation;

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
        SplatRegistrations.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainViewModel>(), "MainContent");
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, IndexViewModel>(ViewName.IndexName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, MemoViewModel>(ViewName.MemoName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, ToDoViewModel>(ViewName.ToDoName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, SettingViewModel>(ViewName.SettingName);
    }
}