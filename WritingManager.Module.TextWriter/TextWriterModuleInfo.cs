using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterModuleInfo<PanelType> : IModuleInfoBase<PanelType>
    {
        public void Registration(ContainerBuilder container)
        {
            container.RegisterGeneric(typeof(TextWriterController<>))
                .As(typeof(IControllerBase<>))
                .SingleInstance();

            container.RegisterType<TextWriterDataWCF>()
                .As<ITextWriterDataConnection>()
                .SingleInstance();

            container.Register(t =>
            {
                switch (t.Resolve<IApplicationConfiguration>().BuildTarget)
                {
                    case ApplicationType.WPF:
                        return new TextWriterViewWPF();
                    default:
                        throw new Exception("Wrong configuration in TextWriter resolving");
                }
            }).As<ITextWriterViewBase<PanelType>>().As<IViewBase<PanelType>>().InstancePerDependency();
        }

        public Type MainControllerType { get; protected set; } = typeof(TextWriterController<PanelType>);
    }
}
