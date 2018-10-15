using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public class ModuleInfoBase<PanelType>
    {
        public virtual void Registration(ContainerBuilder container)
        {
            
        }

        public virtual Type MainControllerType { get; protected set; }
    }
}
