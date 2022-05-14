using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyToDo.Avalonia.Views;

public partial class AboutView : ReactiveUserControl<AboutViewModel>
{
    public AboutView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}