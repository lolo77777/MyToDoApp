using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class MenuBar : AbstractNotifyPropertyChanged
    {
        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set => SetAndRaise(ref _title, value);
        }

        private string _viewModelName = string.Empty;

        public string ViewModelName
        {
            get { return _viewModelName; }
            set => SetAndRaise(ref _viewModelName, value);
        }

        private string _icon = string.Empty;

        public string Icon
        {
            get { return _icon; }
            set => SetAndRaise(ref _icon, value);
        }
    }
}