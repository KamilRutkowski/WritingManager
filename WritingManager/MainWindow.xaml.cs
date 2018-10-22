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
            RefreshModuleBars();
        }

        public List<(string, List<(string, Action)>)> MainToolbarOptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(ControllerBase<Panel>, bool)> LeftModules { get => _leftModules; set { _leftModules = value; RefreshModuleBars(); } }
        private List<(ControllerBase<Panel>, bool)> _leftModules = new List<(ControllerBase<Panel>, bool)>();
        public List<(ControllerBase<Panel>, bool)> RightModules { get => _rightModules; set { _rightModules = value; RefreshRightModuleBar(); } }
        private List<(ControllerBase<Panel>, bool)> _rightModules = new List<(ControllerBase<Panel>, bool)>();
        public event NewModuleClick<Panel> LeftPanelModuleChanged;
        public event NewModuleClick<Panel> RightPanelModuleChanged;
        public Panel LeftPanel { get => _leftPanel;}
        public Panel RightPanel { get => _rightPanel;}

        private void RefreshModuleBars()
        {
            populateToolbar(_leftModules, _leftPanelToolbar, LeftPanelModuleChanged);
            populateToolbar(_rightModules, _rightPanelToolbar, RightPanelModuleChanged);
        }

        private void populateToolbar(List<(ControllerBase<Panel>, bool)> controllers, ToolBar toolBar, NewModuleClick<Panel> moduleChangedDelegate)
        {
            foreach (var module in controllers)
            {
                Button b = new Button();
                b.Content = module.Item1.ModuleName;
                b.IsEnabled = !module.Item2;
                b.Click += (object sender, RoutedEventArgs e) => { moduleChangedDelegate(module.Item1); };
                toolBar.Items.Add(b);
            }
        }

        private void RefreshRightModuleBar()
        {

        }
    }
}
