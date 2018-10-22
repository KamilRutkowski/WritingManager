using Autofac;
using System;

namespace WritingManager.Module
{
    public interface IModuleInfoBase<PanelType>
    {
        void Registration(ContainerBuilder container);

        Type MainControllerType { get; }
    }
}
