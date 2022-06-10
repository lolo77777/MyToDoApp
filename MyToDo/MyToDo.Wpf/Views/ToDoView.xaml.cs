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
            this.Bind(ViewModel, vm => vm.SearchStr, v => v.Search.Text).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.Status, v => v.cbxStatus.SelectedIndex).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.ToDoDtoTitle, v => v.txtToDoDtoTitle.Text).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.ToDoDtoContent, v => v.txtToDoDtoContent.Text).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.ToDoDtoStatus, v => v.cboxToDoDtoStatus.SelectedIndex).DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.IsAddingToDoDto, v => v.btnAddToDoDto.IsEnabled, bol => !bol).DisposeWith(d);
            this.BindCommand(ViewModel, vm => vm.AddToDoDtoCommand, v => v.btnAddToDoDto).DisposeWith(d);
        });
    }
}