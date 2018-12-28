using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WritingManager.Controller;
using WritingManager.Module;
using WritingManager.Module.TextWriter;

namespace WritingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IApplicationView<Panel>
    {
        private ApplicationController<Panel> _controller;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new ApplicationController<Panel>(this);
            RefreshLeftModuleBar();
            RefreshRightModuleBar();
            _moveToLeft.Click += (sender, e) =>
            {
                if (_rightModules.Any(module => module.Item2))
                    MoveModuleToPanel?.Invoke(_rightModules.First(module => module.Item2).Item1, ModuleStatus.LeftPanel);
            };
            _moveToRight.Click += (sender, e) =>
            {
                if (_leftModules.Any(module => module.Item2))
                    MoveModuleToPanel?.Invoke(_leftModules.First(module => module.Item2).Item1, ModuleStatus.RightPanel);
            };
        }

        public List<(string, List<(string, Action)>)> MainToolbarOptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(IControllerBase<Panel>, bool)> LeftModules { get => _leftModules; set { _leftModules = value; RefreshLeftModuleBar(); } }
        private List<(IControllerBase<Panel>, bool)> _leftModules = new List<(IControllerBase<Panel>, bool)>();
        public List<(IControllerBase<Panel>, bool)> RightModules { get => _rightModules; set { _rightModules = value; RefreshRightModuleBar(); } }
        private List<(IControllerBase<Panel>, bool)> _rightModules = new List<(IControllerBase<Panel>, bool)>();
        public event NewModuleClick<Panel> LeftPanelModuleChanged;
        public event NewModuleClick<Panel> RightPanelModuleChanged;
        public event MoveModule<Panel> MoveModuleToPanel;

        public Panel LeftPanel { get => _leftPanel;}
        public Panel RightPanel { get => _rightPanel;}

        private void populateToolbar(List<(IControllerBase<Panel>, bool)> controllers, ToolBar toolBar, NewModuleClick<Panel> moduleChangedDelegate)
        {
            foreach (var module in controllers)
            {
                Button b = new Button();
                b.Content = module.Item1.ModuleName;
                b.IsEnabled = !module.Item2;
                b.Click += (object sender, RoutedEventArgs e) => { moduleChangedDelegate?.Invoke(module.Item1); };
                toolBar.Items.Add(b);
            }
        }

        private void RefreshLeftModuleBar()
        {
            _leftPanelToolbar?.Items.Clear();
            populateToolbar(_leftModules, _leftPanelToolbar, LeftPanelModuleChanged);
        }

        private void RefreshRightModuleBar()
        {
            _rightPanelToolbar?.Items.Clear();
            populateToolbar(_rightModules, _rightPanelToolbar, RightPanelModuleChanged);
        }
    }
}
