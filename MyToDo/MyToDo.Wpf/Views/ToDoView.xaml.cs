namespace MyToDo.Wpf.Views;

/// <summary>
/// ToDoView.xaml 的交互逻辑
/// </summary>
public partial class ToDoView
{
    public ToDoView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, vm => vm.ToDoDtos, v => v.itemControlToDo.ItemsSource).DisposeWith(d);
            this.BindCommand(ViewModel, vm => vm.AddToDoCommand, v => v.btnAddToDo).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.IsDrawerHostAddToDoIsOpen, v => v.drawerRightAddToDo.IsRightDrawerOpen).DisposeWith(d);
        });
    }
}