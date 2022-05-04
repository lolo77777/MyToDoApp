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
    }
}