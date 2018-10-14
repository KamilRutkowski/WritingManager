using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterViewBase<PanelType>: ViewBase<PanelType>
    {
        public virtual event Action Save;
        public virtual string Data { get; protected set; }
    }
}
