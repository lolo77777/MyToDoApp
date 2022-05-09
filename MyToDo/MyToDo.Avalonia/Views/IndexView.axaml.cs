using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyToDo.Avalonia.Views;

public partial class IndexView : ReactiveUserControl<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();
        var itemControlTaskBar = this.FindControl<ItemsControl>("itemControlTaskBar");
        this.WhenActivated(d =>
        {
            //this.OneWayBind(ViewModel, vm => vm.TaskBars, v => v.itemControlTaskBar.Items).DisposeWith(d);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}