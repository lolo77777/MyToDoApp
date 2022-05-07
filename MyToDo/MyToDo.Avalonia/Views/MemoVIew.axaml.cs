namespace MyToDo.Avalonia.Views;

public partial class MemoView : ReactiveUserControl<MemoViewModel>
{
    public MemoView()
    {
        InitializeComponent();
        this.WhenActivated(d => { });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}