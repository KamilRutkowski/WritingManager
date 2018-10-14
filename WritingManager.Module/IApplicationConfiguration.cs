using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public interface IApplicationConfiguration
    {
        ApplicationType BuildTarget { get; set; }
    }

    [Flags]
    public enum ApplicationType
    {
        WPF = 1,
        Other = 2
    }
}
