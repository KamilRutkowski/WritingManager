using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public interface IViewBase<PanelType>
    {
        PanelType Panel { set; }
        void Initialize();
        void Show();
        void Hide();
    }
}
