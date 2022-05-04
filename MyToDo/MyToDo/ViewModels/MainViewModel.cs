namespace MyToDo.ViewModels;

public class MainViewModel : ReactiveObject
{
    [Reactive] public string TestStr { get; set; } = "hello";
}