using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Windows.Controls;
using WritingManager.Module;

namespace WritingManager
{
    public class ApplicationController<PanelType>
    {
        private ApplicationConfiguration _configuration { get; set; } = new ApplicationConfiguration();
        private ContainerBuilder _container { get; set; } = new ContainerBuilder();
        private List<(ControllerBase<PanelType>, ModuleStatus)> _activeModules { get; set; } = new List<(ControllerBase<PanelType>, ModuleStatus)>();
        private ControllerBase<PanelType> _activeLeftModule { get; set; } = null;
        private ControllerBase<PanelType> _activeRightModule { get; set; } = null;

        private IApplicationView<PanelType> _applicationView;

        public ApplicationController(IApplicationView<PanelType> view)
        {
            _applicationView = view;
            Initialize();
        }

        private void Initialize()
        {
            LoadConfiguration();
            RegisterModules();
            RecreateLastKnownLayout();
        }

        private void LoadConfiguration()
        {

        }

        private void SaveConfiguration()
        {

        }

        private void RegisterModules()
        {
            _container.RegisterInstance(_configuration)
                .As<IApplicationConfiguration>();
            foreach (var module in _configuration.RegisteredModulesInfoBases)
            {
                module.Item1.Registration(_container);
            }
            _container.Build();
        }

        private void RecreateLastKnownLayout()
        {

        }
    }
}
