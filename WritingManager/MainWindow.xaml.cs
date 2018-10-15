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
        }

        public List<(string, List<(string, Action)>)> MainToolbarOptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(string, Action, bool)> LeftModules { get => _leftModules; set => _leftModules = value; }
        private List<(string, Action, bool)> _leftModules = new List<(string, Action, bool)>();
        public List<(string, Action, bool)> RightModules { get => _rightModules; set => _rightModules = value; }
        private List<(string, Action, bool)> _rightModules = new List<(string, Action, bool)>();
        public Panel LeftPanel { get => _leftPanel;}
        public Panel RightPanel { get => _rightPanel;}
    }
}
