using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager
{
    public interface IApplicationView<PanelType>
    {
        List<(string, List<(string, Action)>)> MainToolbarOptions { get; set; }
        List<(string, Action)> LeftModules { get; set; }
        List<(string, Action)> RightModules { get; set; }
        PanelType LeftPanel { get; set; }
        PanelType RightPanel { get; set; }
    }
}
