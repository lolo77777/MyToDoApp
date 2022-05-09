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
        });
    }
}