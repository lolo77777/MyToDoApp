using Avalonia.Interactivity;
using Avalonia.Input;

namespace MyToDo.Avalonia.Views;

public partial class MainView : ReactiveWindow<MainViewModel>
{
    private bool _isDragMoving;

    public MainView()
    {
        InitializeComponent();
        ViewModel = Current.GetService<MainViewModel>();
        this.PointerMoved += (s, e) => _isDragMoving = e.Pointer.IsPrimary;

        this.PointerPressed += (s, e) =>
        {
            if (_isDragMoving)
            {
                BeginMoveDrag(e);
            }
        };
        this.WhenActivated(d =>
        {
        });
    }

    private void OnButtonMinClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void OnButtonMaxClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void OnButtonCloseClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}