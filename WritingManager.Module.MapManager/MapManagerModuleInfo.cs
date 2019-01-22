using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace WritingManager.Module.MapManager
{
    public class MapManagerModuleInfo<PanelType> : IModuleInfoBase<PanelType>
    {
        public void Registration(ContainerBuilder container)
        {
            container.RegisterGeneric(typeof(MapManagerController<>))
                .As(typeof(IControllerBase<>))
                .SingleInstance();

            container.RegisterType<MapManagerDataWCF>()
                .As<IMapManagerDataConnection>()
                .SingleInstance();

            container.Register(t =>
            {
                switch (t.Resolve<IApplicationConfiguration>().BuildTarget)
                {
                    case ApplicationType.WPF:
                        return new MapManagerViewWPF();
                    default:
                        throw new Exception("Wrong configuration in TextWriter resolving");
                }
            }).As<IMapManagerViewBase<PanelType>>().As<IViewBase<PanelType>>().InstancePerDependency();
        }

        public Type MainControllerType { get; protected set; } = typeof(MapManagerController<PanelType>);
    }
}
