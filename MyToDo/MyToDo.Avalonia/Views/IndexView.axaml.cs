using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyToDo.Avalonia.Views;

public partial class IndexView : ReactiveUserControl<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();
        this.WhenActivated(d => { });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}