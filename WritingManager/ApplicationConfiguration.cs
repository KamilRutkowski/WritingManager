using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingManager.Module;
using WritingManager.Module.TextWriter;

namespace WritingManager
{
    public class ApplicationConfiguration: IApplicationConfiguration
    {
        public ApplicationType BuildTarget { get; set; } = ApplicationType.WPF;
        public List<(ModuleInfoBase, ModuleStatus)> RegisteredModulesInfoBases { get; set; } = new List<(ModuleInfoBase, ModuleStatus)>
        {
            (new TextWriterModuleInfo(), ModuleStatus.LeftPanel)
        };
    }

    public enum ModuleStatus
    {
        LeftPanel = 1,
        RightPanel = 2,
        NotAssigned = 4
    }
}
