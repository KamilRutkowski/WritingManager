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
        private List<(IControllerBase<PanelType>, ModuleStatus)> _modules { get; set; } = new List<(IControllerBase<PanelType>, ModuleStatus)>();
        private IControllerBase<PanelType> _activeLeftModule { get; set; } = null;
        private IControllerBase<PanelType> _activeRightModule { get; set; } = null;

        private IApplicationView<PanelType> _applicationView;

        public ApplicationController(IApplicationView<PanelType> view)
        {
            _applicationView = view;
            Initialize();
            _applicationView.MoveModuleToPanel += (module, target) =>
            {
                if (target == ModuleStatus.LeftPanel)
                {
                    foreach (var mod in _modules)
                        if ((mod.Item2 & ModuleStatus.Active) == ModuleStatus.Active)
                            mod.Item1.UnloadFromPanel();
                    _modules.Remove(_modules.First(mod => mod.Item1 == module));
                    _modules.Add((module, ModuleStatus.LeftPanel));
                    _activeRightModule = null;
                    SetPanels();
                    PopulateModuleToolbars();
                }
                else if (target == ModuleStatus.RightPanel)
                {
                    foreach (var mod in _modules)
                        if ((mod.Item2 & ModuleStatus.Active) == ModuleStatus.Active)
                            mod.Item1.UnloadFromPanel();
                    _modules.Remove(_modules.First(mod => mod.Item1 == module));
                    _modules.Add((module, ModuleStatus.RightPanel));
                    _activeLeftModule = null;
                    SetPanels();
                    PopulateModuleToolbars();
                }
            };
        }

        private void Initialize()
        {
            LoadConfiguration();
            RegisterModules();
            RecreateLastKnownLayout();
            ConfigureEvents();
        }

        private void ConfigureEvents()
        {
            _applicationView.LeftPanelModuleChanged += (IControllerBase<PanelType> controller) => 
            {
                if (_activeLeftModule != null)
                {
                    _activeLeftModule.UnloadFromPanel();
                    var oldModIndex = _modules.FindIndex(mt => mt.Item1 == _activeLeftModule);
                    _modules[oldModIndex] = (_activeLeftModule, ModuleStatus.LeftPanel);
                }
                var newModIndex = _modules.FindIndex(mt => mt.Item1 == controller);
                _modules[newModIndex] = (controller, ModuleStatus.LeftPanel | ModuleStatus.Active);
                _activeLeftModule = controller;
                controller.ShowOnPanel(_applicationView.LeftPanel);
                PopulateModuleToolbars();
                SaveConfiguration();
            };
            _applicationView.RightPanelModuleChanged += (IControllerBase<PanelType> controller) =>
            {
                if (_activeRightModule != null)
                {
                    _activeRightModule.UnloadFromPanel();
                    var oldModIndex = _modules.FindIndex(mt => mt.Item1 == _activeRightModule);
                    _modules[oldModIndex] = (_activeRightModule, ModuleStatus.RightPanel);
                }
                var newModIndex = _modules.FindIndex(mt => mt.Item1 == controller);
                _modules[newModIndex] = (controller, ModuleStatus.RightPanel | ModuleStatus.Active);
                _activeRightModule = controller;
                controller.ShowOnPanel(_applicationView.RightPanel);
                PopulateModuleToolbars();
                SaveConfiguration();
            };
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
                .Resolve<IList<IControllerBase<PanelType>>>()
                .Select(controller =>
                    (controller,
                    _configuration.RegisteredModulesInfoBases.First(mod => controller.GetType() == mod.Item1.MainControllerType).Item2)).ToList();
            SetPanels();
            PopulateModuleToolbars();
        }

        private void SetPanels()
        {
            if (_modules.Any(modules => modules.Item2 == (ModuleStatus.LeftPanel | ModuleStatus.Active)))
                _activeLeftModule = _modules.FirstOrDefault(modules => modules.Item2 == (ModuleStatus.LeftPanel | ModuleStatus.Active)).Item1;
            if (_modules.Any(modules => modules.Item2 == (ModuleStatus.RightPanel | ModuleStatus.Active)))
                _activeRightModule = _modules.FirstOrDefault(modules => modules.Item2 == (ModuleStatus.RightPanel | ModuleStatus.Active)).Item1;
            if (_activeLeftModule != null)
                _activeLeftModule.ShowOnPanel(_applicationView.LeftPanel);
            if (_activeRightModule != null)
                _activeRightModule.ShowOnPanel(_applicationView.RightPanel);
        }

        private void PopulateModuleToolbars()
        {
            _applicationView.LeftModules = _modules.
                Where(m => (m.Item2 & ModuleStatus.LeftPanel) != 0)
                .Select(module => (module.Item1, (module.Item2 & ModuleStatus.Active) != 0 ? true : false))
                .ToList();
            _applicationView.RightModules = _modules.
                Where(m => (m.Item2 & ModuleStatus.RightPanel) != 0)
                .Select(module => (module.Item1, (module.Item2 & ModuleStatus.Active) != 0 ? true : false))
                .ToList();
        }
    }
}
