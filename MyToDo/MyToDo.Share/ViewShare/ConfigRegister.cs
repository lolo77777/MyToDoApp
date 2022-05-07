namespace MyToDo.Share.ViewShare;

public static class ConfigRegister
{
    public static void RegisterViewForViewModel()
    {
        CurrentMutable.Register<IViewFor<IndexViewModel>, IndexView>();
        CurrentMutable.Register<IViewFor<SettingViewModel>, SettingView>();
        CurrentMutable.Register<IViewFor<ToDoViewModel>, ToDoView>();
        CurrentMutable.Register<IViewFor<MemoViewModel>, MemoView>();
    }
}