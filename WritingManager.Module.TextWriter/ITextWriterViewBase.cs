using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.TextWriter
{
    public interface ITextWriterViewBase<PanelType>: IViewBase<PanelType>
    {
        event Action Save;
        string Data { get; }
    }
}
