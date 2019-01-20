using DatabaseConnectorServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WritingManager.WPF.Common.Dialogs;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerViewWPF : ICharactersManagerViewBase<Panel>
    {
        public Panel Panel { protected get; set; }

        public event Action Save;
        public event Action Load;
        public event Action NewCharacter;

        public string Name {
            get
            {
                return _name.Text;
            }
            set
            {
                _name.Text = value;
            }
        }

        public string BaseInformation
        {
            get
            {
                return _baseInformation.Text;
            }
            set
            {
                _baseInformation.Text = value;

            }
        }

        public string Appearance
        {
            get
            {
                return _appearance.Text;
            }
            set
            {
                _appearance.Text = value;
            }
        }

        public string Description
        {
            get
            {
                return _description.Text;
            }
            set
            {
                _description.Text = value;
            }
        }

        private Grid _gridPanel;
        private ToolBar _toolBar;
        private MenuItem _newCharacter;
        private MenuItem _saveFile;
        private MenuItem _loadFile;
        private MenuItem _fileMenuItem;
        private Menu _fileMenu;

        private TextBox _name;
        private TextBox _baseInformation;
        private TextBox _appearance;
        private TextBox _description;

        public CharactersManagerViewWPF()
        {
            Initialize();
        }

        public void Initialize()
        {
            #region Toolbar setup
            _fileMenu = new Menu();

            _fileMenuItem = new MenuItem();
            _fileMenuItem.Header = "File";

            _saveFile = new MenuItem();
            _saveFile.Click += (sender, args) => { Save?.Invoke(); };
            _saveFile.Header = "Save character";

            _loadFile = new MenuItem();
            _loadFile.Click += (sender, args) => { Load?.Invoke(); };
            _loadFile.Header = "Load character";

            _newCharacter = new MenuItem();
            _newCharacter.Click += (sender, args) => { NewCharacter?.Invoke(); };
            _newCharacter.Header = "New character";

            _fileMenuItem.Items.Add(_newCharacter);
            _fileMenuItem.Items.Add(_saveFile);
            _fileMenuItem.Items.Add(_loadFile);
            _fileMenu.Items.Add(_fileMenuItem);

            _toolBar = new ToolBar();
            _toolBar.Items.Add(_fileMenu);
            Grid.SetColumn(_toolBar, 0);
            Grid.SetRow(_toolBar, 0);
            #endregion

            #region Content setup

            StackPanel contentLayout = new StackPanel();
            Grid.SetColumn(contentLayout, 0);
            Grid.SetRow(contentLayout, 1);

            Label nameLabel = new Label();
            nameLabel.Content = "Character name:";
            contentLayout.Children.Add(nameLabel);
            _name = new TextBox();
            contentLayout.Children.Add(_name);

            Label baseInfoLabel = new Label();
            baseInfoLabel.Content = "Base information:";
            contentLayout.Children.Add(baseInfoLabel);
            _baseInformation = new TextBox();
            _baseInformation.Height = 70;
            _baseInformation.TextWrapping = TextWrapping.Wrap;
            _baseInformation.AcceptsReturn = true;
            contentLayout.Children.Add(_baseInformation);

            Label appearanceLabel = new Label();
            appearanceLabel.Content = "Character appearance:";
            contentLayout.Children.Add(appearanceLabel);
            _appearance = new TextBox();
            _appearance.Height = 70;
            _appearance.TextWrapping = TextWrapping.Wrap;
            _appearance.AcceptsReturn = true;
            contentLayout.Children.Add(_appearance);

            Label desctiptionLabel = new Label();
            desctiptionLabel.Content = "Character description:";
            contentLayout.Children.Add(desctiptionLabel);
            _description = new TextBox();
            _description.Height = 300;
            _description.TextWrapping = TextWrapping.Wrap;
            _description.AcceptsReturn = true;
            contentLayout.Children.Add(_description);


            #endregion

            #region Grid setup
            _gridPanel = new Grid();

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = GridLength.Auto;
            _gridPanel.RowDefinitions.Add(rd1);
            RowDefinition rd2 = new RowDefinition();
            _gridPanel.RowDefinitions.Add(rd2);
            ColumnDefinition cd1 = new ColumnDefinition();
            _gridPanel.ColumnDefinitions.Add(cd1);
            _gridPanel.Children.Add(_toolBar);
            _gridPanel.Children.Add(contentLayout);
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

        public bool ClearFormPrompt()
        {
            return MessageBoxResult.Yes == MessageBox.Show("Clear data in preparation for new character?", "Clear form", MessageBoxButton.YesNo);
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
