namespace MyToDo.Wpf.Views
{
    /// <summary>
    /// MemoView.xaml 的交互逻辑
    /// </summary>
    public partial class MemoView
    {
        public MemoView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.MemoDtos, v => v.itemControlToDo.ItemsSource).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.AddToDoCommand, v => v.btnAddToDo).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsDrawerHostAddToDoIsOpen, v => v.drawerRightAddToDo.IsRightDrawerOpen).DisposeWith(d);
            });
        }
    }
}