namespace MyToDo.Avalonia.Views;

public partial class SkinView : ReactiveUserControl<SkinViewModel>
{
    public SkinView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}