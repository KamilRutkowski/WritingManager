using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WritingManager.Module;

namespace WritingManager.Controller
{
    public class ApplicationController<PanelType>
    {
        private ApplicationConfiguration<PanelType> _configuration { get; set; } = new ApplicationConfiguration<PanelType>();
        private IContainer _container { get; set; }
        private List<(ControllerBase<PanelType>, ModuleStatus)> _modules { get; set; } = new List<(ControllerBase<PanelType>, ModuleStatus)>();
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
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(_configuration)
                .As<IApplicationConfiguration>();
            foreach (var module in _configuration.RegisteredModulesInfoBases)
            {
                module.Item1.Registration(containerBuilder);
            }
            _container = containerBuilder.Build();
        }

        private void RecreateLastKnownLayout()
        {
            _modules = _container
                .Resolve<IList<ControllerBase<PanelType>>>()
                .Select(controller =>
                    (controller,
                    _configuration.RegisteredModulesInfoBases.First(mod => controller.GetType() == mod.Item1.MainControllerType).Item2)).ToList();
            if (_modules.Any(modules => modules.Item2 == (ModuleStatus.LeftPanel | ModuleStatus.Active)))
                _activeLeftModule = _modules.FirstOrDefault(modules => modules.Item2 == (ModuleStatus.LeftPanel | ModuleStatus.Active)).Item1;
            if (_modules.Any(modules => modules.Item2 == (ModuleStatus.RightPanel | ModuleStatus.Active)))
                _activeRightModule = _modules.FirstOrDefault(modules => modules.Item2 == (ModuleStatus.RightPanel | ModuleStatus.Active)).Item1;
            if (_activeLeftModule != null)
                _activeLeftModule.ShowOnPanel(_applicationView.LeftPanel);
            if (_activeRightModule != null)
                _activeRightModule.ShowOnPanel(_applicationView.RightPanel);
        }
    }
}
