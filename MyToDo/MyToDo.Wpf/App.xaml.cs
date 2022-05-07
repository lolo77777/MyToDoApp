namespace MyToDo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        _ = new BootStarpper();
        ConfigRegister.RegisterViewForViewModel();
    }
}