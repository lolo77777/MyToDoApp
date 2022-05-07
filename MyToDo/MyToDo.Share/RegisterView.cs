using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Share
{
    public static class RegisterView
    {
        public static void Config()
        {
            CurrentMutable.Register<IViewFor<IndexViewModel>, IndexView>();
            CurrentMutable.Register<IViewFor<SettingViewModel>, SettingView>();
            CurrentMutable.Register<IViewFor<ToDoViewModel>, ToDoView>();
            CurrentMutable.Register<IViewFor<MemoViewModel>, MemoView>();
        }
    }
}