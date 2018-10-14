using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public class ViewBase<PanelType>
    {
        public PanelType Panel { protected get; set; }

        public ViewBase() { }

        public virtual void Initialize() { }

        public virtual void Show() { }

        public virtual void Hide() { }
    }
}
