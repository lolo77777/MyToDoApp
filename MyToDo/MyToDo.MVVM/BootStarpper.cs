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