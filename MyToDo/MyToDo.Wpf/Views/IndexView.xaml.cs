namespace MyToDo.Wpf.Views;

[SingleInstanceView]
public partial class IndexView
{
    public IndexView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, vm => vm.TaskBars, v => v.itemControlTaskBar.ItemsSource).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.ToDoDtos, v => v.listBoxToDo.ItemsSource).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.MemoDtos, v => v.listBoxMemo.ItemsSource).DisposeWith(d);
        });
    }
}