using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public interface IControllerBase<PanelType>
    {
        string ModuleName { get; }
        void ShowOnPanel(PanelType panel);
        void UnloadFromPanel();
        void RegisterShortcuts(IList<Shortcut<PanelType>> shortcuts);
        bool PendingChanges();
    }
}
