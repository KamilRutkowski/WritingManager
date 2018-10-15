using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Controller
{
    public interface IApplicationView<PanelType>
    {
        List<(string, List<(string, Action)>)> MainToolbarOptions { get; set; }
        List<(string, Action, bool)> LeftModules { get; set; }
        List<(string, Action, bool)> RightModules { get; set; }
        PanelType LeftPanel { get; }
        PanelType RightPanel { get; }
    }
}
