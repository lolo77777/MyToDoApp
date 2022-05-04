using System.Reactive.Linq;

namespace MyToDo.Views;

/// <summary>
/// MainView.xaml 的交互逻辑
/// </summary>
public partial class MainView : Window, IViewFor<MainViewModel>
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty
       .Register(nameof(ViewModel), typeof(MainViewModel), typeof(MainView));

    public MainView()
    {
        InitializeComponent();
        ViewModel = Current.GetService<MainViewModel>() ?? new MainViewModel();
        this.WhenActivated(d =>
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            btnClose.Click += (o, e) => this.Close();
            btnMin.Click += (o, e) => this.WindowState = WindowState.Minimized;
            btnMax.Click += (o, e) => this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            titleBar.MouseDoubleClick += (o, e) => this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            this.Events().MouseMove
                .Where(e => e.LeftButton == MouseButtonState.Pressed)
                .Subscribe(_ => this.DragMove());
        });
    }

    public MainViewModel ViewModel
    {
        get => (MainViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (MainViewModel)value;
    }
}