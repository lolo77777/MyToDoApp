namespace MyToDo.Avalonia.Views;

public partial class ToDoView : ReactiveUserControl<ToDoViewModel>
{
    public ToDoView()
    {
        InitializeComponent();
        this.WhenActivated(d => { });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}