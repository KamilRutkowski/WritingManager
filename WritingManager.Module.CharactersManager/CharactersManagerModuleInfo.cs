using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerModuleInfo<PanelType> : IModuleInfoBase<PanelType>
    {
        public void Registration(ContainerBuilder container)
        {
            container.RegisterType<CharactersManagerDatabaseConnection>()
                .As<ICharactersManagerDatabaseConnection>()
                .SingleInstance();

            container.RegisterGeneric(typeof(CharactersManagerController<>))
                .As(typeof(IControllerBase<>))
                .SingleInstance();

            container.Register(t =>
            {
                switch (t.Resolve<IApplicationConfiguration>().BuildTarget)
                {
                    case ApplicationType.WPF:
                        return new CharactersManagerViewWPF();
                    default:
                        throw new Exception("Wrong configuration in TextWriter resolving");
                }
            }).As<ICharactersManagerViewBase<PanelType>>().As<IViewBase<PanelType>>().InstancePerDependency();
        }

        public Type MainControllerType { get; protected set; } = typeof(CharactersManagerController<PanelType>);
    }
}
