namespace MyToDo;

public class BootStarpper
{
    public BootStarpper()
    {
        ConfigerRegister();
    }

    private static void ConfigerRegister()
    {
        CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        CurrentMutable.RegisterLazySingleton(() => new MainViewModel());
        CurrentMutable.RegisterLazySingleton<IRoutableViewModel>(() => new IndexViewModel(), nameof(IndexViewModel));
        CurrentMutable.RegisterLazySingleton<IRoutableViewModel>(() => new MemoViewModel(), nameof(MemoViewModel));
        CurrentMutable.RegisterLazySingleton<IRoutableViewModel>(() => new ToDoViewModel(), nameof(ToDoViewModel));
        CurrentMutable.RegisterLazySingleton<IRoutableViewModel>(() => new SettingViewModel(), nameof(SettingViewModel));
    }
}