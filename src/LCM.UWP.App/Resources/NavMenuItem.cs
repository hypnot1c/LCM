using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LCM.UWP.App.Resources
{
  public class NavMenuItem
  {
    public Symbol Icon { get; set; }
    public char IconAsChar => (char)Icon;

    public Type TargetViewModel { get; set; }
    public string Title { get; set; }
  }
}
