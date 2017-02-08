using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Mvvm;

namespace LCM.UWP_App.ViewModels
{
  public class MainPageViewModel: ViewModelBase
  {
    public MainPageViewModel()
    {
      DisplayText = "This is the main page!";
    }
    public string DisplayText { get; private set; }
  }
}
