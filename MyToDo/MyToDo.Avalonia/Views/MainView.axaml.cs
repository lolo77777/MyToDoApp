namespace MyToDo.Avalonia.Views;

public partial class MainView : ReactiveWindow<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
        ViewModel = Current.GetService<MainViewModel>();
        this.WhenActivated(d =>
        {
        });
    }
}