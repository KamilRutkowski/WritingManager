﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterModuleInfo : ModuleInfoBase
    {
        public override void Registration(ContainerBuilder container)
        {
            container.RegisterType<TextWriterDatabaseConnection>()
                .As<ITextWriterDatabaseConnection>()
                .SingleInstance();

            container.RegisterGeneric(typeof(TextWriterController<>))
                .As(typeof(ControllerBase<>))
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
            }).As(typeof(TextWriterViewBase<>)).InstancePerDependency();
        }
    }
}
