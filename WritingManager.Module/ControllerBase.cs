using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public class ControllerBase<PanelType>
    {
        public virtual string ModuleName { get; protected set; }

        public ControllerBase(ViewBase<PanelType> view, IDatabaseConnection database) { }
        public virtual void ShowOnPanel(PanelType panel) { }
        public virtual void UnloadFromPanel() { }
        public virtual void RegisterShortcuts(IList<Shortcut<PanelType>> shortcuts) { }
        public virtual bool PendingChanges() { return false; }
    }
}
