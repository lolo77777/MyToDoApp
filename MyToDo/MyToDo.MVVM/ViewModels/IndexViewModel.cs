using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.MVVM.ViewModels;

public class IndexViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}