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
            //TextWriterViewWPF twView = new TextWriterViewWPF();
            //TextWriterController<Panel> controller = new TextWriterController<Panel>(twView, new TextWriterDatabaseConnection());
            //controller.ShowOnPanel(_leftPanel);
        }

        public List<(string, List<(string, Action)>)> MainToolbarOptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(string, Action)> LeftModules { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(string, Action)> RightModules { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Panel LeftPanel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Panel RightPanel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
