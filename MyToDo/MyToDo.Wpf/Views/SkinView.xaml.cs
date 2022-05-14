namespace MyToDo.Wpf.Views;

/// <summary>
/// SkinView.xaml 的交互逻辑
/// </summary>
public partial class SkinView
{
    private readonly PaletteHelper paletteHelper = new();
    public ReactiveCommand<object, Unit> ChangeHueCommand { set; get; }

    public SkinView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            DataContext = this;
            itemsControlSwatchs.ItemsSource = SwatchHelper.Swatches;
            this.WhenAnyValue(x => x.togSwitchTheme.IsChecked)
                .Select(b => b == true)
                .Subscribe(b => ModifyTheme(theme => theme.SetBaseTheme(b ? Theme.Dark : Theme.Light)))
                .DisposeWith(d);
            ChangeHueCommand = ReactiveCommand.Create<object, Unit>(ChangeHue);
        });
    }

    private static void ModifyTheme(Action<ITheme> modificationAction)
    {
        var paletteHelper = new PaletteHelper();
        ITheme theme = paletteHelper.GetTheme();
        modificationAction?.Invoke(theme);
        paletteHelper.SetTheme(theme);
    }

    private Unit ChangeHue(object obj)
    {
        var hue = (Color)obj;
        ITheme theme = paletteHelper.GetTheme();
        theme.PrimaryLight = new ColorPair(hue.Lighten());
        theme.PrimaryMid = new ColorPair(hue);
        theme.PrimaryDark = new ColorPair(hue.Darken());
        paletteHelper.SetTheme(theme);
        return Unit.Default;
    }
}