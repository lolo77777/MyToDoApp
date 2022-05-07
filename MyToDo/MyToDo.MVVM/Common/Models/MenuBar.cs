using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.MVVM.Common.Models
{
    public class MenuBar
    {
        public string Title { get; set; } = string.Empty;

        public string ViewName { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}