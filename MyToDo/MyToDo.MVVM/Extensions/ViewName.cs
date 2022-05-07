namespace MyToDo.MVVM.Extensions;

public static class ViewName
{
    public static string IndexName { get; set; } = nameof(IndexViewModel);
    public static string MemoName { get; set; } = nameof(MemoViewModel);
    public static string ToDoName { get; set; } = nameof(ToDoViewModel);
    public static string SettingName { get; set; } = nameof(SettingViewModel);
}