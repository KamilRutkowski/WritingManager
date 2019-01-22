using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingManager.Module;

namespace WritingManager.Controller
{
    public interface IApplicationView<PanelType>
    {
        List<(string, List<(string, Action)>)> MainToolbarOptions { get; set; }
        List<(IControllerBase<PanelType>, bool)> LeftModules { get; set; }
        List<(IControllerBase<PanelType>, bool)> RightModules { get; set; }
        event NewModuleClick<PanelType> LeftPanelModuleChanged;
        event NewModuleClick<PanelType> RightPanelModuleChanged;
        event MoveModule<PanelType> MoveModuleToPanel;
        PanelType LeftPanel { get; }
        PanelType RightPanel { get; }
    }

    public delegate void NewModuleClick<PanelType>(IControllerBase<PanelType> controllerBase);
    public delegate void MoveModule<PanelType>(IControllerBase<PanelType> controllerBase, ModuleStatus target);
}
