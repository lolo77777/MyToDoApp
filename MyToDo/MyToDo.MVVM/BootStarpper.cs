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

        SplatRegistrations.RegisterLazySingleton<MainViewModel>();
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, IndexViewModel>(ViewName.IndexName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, MemoViewModel>(ViewName.MemoName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, ToDoViewModel>(ViewName.ToDoName);
        SplatRegistrations.RegisterLazySingleton<IRoutableViewModel, SettingViewModel>(ViewName.SettingName);
    }
}