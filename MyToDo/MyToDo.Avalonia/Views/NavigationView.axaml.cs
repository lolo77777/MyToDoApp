namespace MyToDo.Avalonia.Views
{
    public partial class NavigationView : ReactiveUserControl<NavigationViewModel>
    {
        public NavigationView()
        {
            InitializeComponent();
            this.WhenActivated(d => { });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}