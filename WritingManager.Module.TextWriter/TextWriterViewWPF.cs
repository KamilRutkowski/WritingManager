using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WritingManager.Module;
using WritingManager.WPF.Common.Dialogs;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterViewWPF : ITextWriterViewBase<Panel>
    {
        public string Data
        {
            get
            {
                return _textBox.Text;
            }
            set
            {
                _textBox.Text = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileNameTextBox.Text;
            }
            set
            {
                _fileNameTextBox.Text = value;
            }
        }

        public Panel Panel { protected get; set; }

        public event Action Save;
        public event Action SaveNew;
        public event Action Load;

        private Grid _gridPanel;
        private ToolBar _toolBar;
        private TextBox _textBox;
        private MenuItem _saveFile;
        private MenuItem _loadFile;
        private MenuItem _saveNewFile;
        private MenuItem _fileMenuItem;
        private TextBox _fileNameTextBox;

        private Menu _fileMenu;

        public TextWriterViewWPF()
        {
            Initialize();
        }

        public void Initialize()
        {
            #region ToolbarSetup
            _fileMenu = new Menu();

            _fileMenuItem = new MenuItem();
            _fileMenuItem.Header = "File";

            _saveFile = new MenuItem();
            _saveFile.Click += (sender, args) => { Save?.Invoke(); };
            _saveFile.Header = "Save document";

            _loadFile = new MenuItem();
            _loadFile.Click += (sender, args) => { Load?.Invoke(); };
            _loadFile.Header = "Load document";

            _saveNewFile = new MenuItem();
            _saveNewFile.Click += (sender, args) => { SaveNew?.Invoke(); };
            _saveNewFile.Header = "Save as new document";

            _fileNameTextBox = new TextBox();
            _fileNameTextBox.Width = 100;
            _fileNameTextBox.IsReadOnly = true;

            var fileNameLabel = new Label();
            fileNameLabel.Content = "Current document name:";

            _fileMenuItem.Items.Add(_saveFile);
            _fileMenuItem.Items.Add(_saveNewFile);
            _fileMenuItem.Items.Add(_loadFile);
            _fileMenu.Items.Add(_fileMenuItem);

            _toolBar = new ToolBar();
            _toolBar.Items.Add(_fileMenu);
            _toolBar.Items.Add(fileNameLabel);
            _toolBar.Items.Add(_fileNameTextBox);
            Grid.SetColumn(_toolBar, 0);
            Grid.SetRow(_toolBar, 0);
            #endregion

            #region TextBoxSetup
            _textBox = new TextBox();
            _textBox.Margin = new Thickness(0.2);
            _textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            _textBox.VerticalAlignment = VerticalAlignment.Stretch;
            _textBox.TextWrapping = TextWrapping.Wrap;
            _textBox.AcceptsReturn = true;
            
            _textBox.MinWidth = 300;
            Grid.SetColumn(_textBox, 0);
            Grid.SetRow(_textBox, 1);

            #endregion

            #region GridSetup
            _gridPanel = new Grid();

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = GridLength.Auto;
            _gridPanel.RowDefinitions.Add(rd1);
            RowDefinition rd2 = new RowDefinition();
            _gridPanel.RowDefinitions.Add(rd2);
            ColumnDefinition cd1 = new ColumnDefinition();
            _gridPanel.ColumnDefinitions.Add(cd1);
            _gridPanel.Children.Add(_toolBar);
            _gridPanel.Children.Add(_textBox);
            _gridPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            _gridPanel.VerticalAlignment = VerticalAlignment.Stretch;
            _gridPanel.Margin = new Thickness(0.2);
            #endregion
        }

        public void Show()
        {
            Panel.Children.Add(_gridPanel);
        }

        public void Hide()
        {
            Panel.Children.Clear();
        }

        public string SaveFileName()
        {
            var dialog = new ChooseFileDialog();
            dialog.ShowDialog();
            return dialog.DocumentName;
        }

        public (bool, FileData) LoadFile(IEnumerable<FileData> textFileInfos)
        {
            var dialog = new ChooseFileAndDateDialog();
            dialog.DataStore = textFileInfos.ToList();
            dialog.ShowDialog();
            return (dialog.WasOk, dialog.ChoosenFileData);
        }
    }
}
