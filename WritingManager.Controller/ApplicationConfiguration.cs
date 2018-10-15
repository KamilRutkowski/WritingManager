using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingManager.Module;
using WritingManager.Module.TextWriter;

namespace WritingManager.Controller
{
    public class ApplicationConfiguration<PanelType>: IApplicationConfiguration
    {
        public ApplicationType BuildTarget { get; set; } = ApplicationType.WPF;
        public List<(ModuleInfoBase<PanelType>, ModuleStatus)> RegisteredModulesInfoBases { get; set; } = new List<(ModuleInfoBase<PanelType>, ModuleStatus)>
        {
            (new TextWriterModuleInfo<PanelType>(), ModuleStatus.RightPanel | ModuleStatus.Active)
        };
    }

    [Flags]
    public enum ModuleStatus
    {
        LeftPanel = 1,
        RightPanel = 2,
        NotAssigned = 4,
        Active = 8
    }
}
