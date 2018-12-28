using DatabaseConnectorServiceWCF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WritingManager.WPF.Common.Dialogs;

namespace WritingManager.Module.MapManager
{
    public class MapManagerViewWPF : IMapManagerViewBase<Panel>
    {
        public Panel Panel { set; protected get; }
        public string ImageName { set { _fileNameTextBox.Text = value; } }
        public byte[] ImageArray { set
            {
                var imSource = new BitmapImage();
                using (var mem = new MemoryStream(value))
                {
                    mem.Position = 0;
                    imSource.BeginInit();
                    imSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    imSource.CacheOption = BitmapCacheOption.OnLoad;
                    imSource.UriSource = null;
                    imSource.StreamSource = mem;
                    imSource.EndInit();
                }
                imSource.Freeze();
                _imageBox.Source = imSource;
            }
        }

        public event Action Add;
        public event Action Load;

        private Grid _gridPanel;
        private ToolBar _toolBar;
        private Image _imageBox;
        private MenuItem _addImage;
        private MenuItem _loadImage;
        private MenuItem _fileMenuItem;
        private TextBox _fileNameTextBox;
        private Menu _fileMenu;

        public MapManagerViewWPF()
        {
            Initialize();
        }

        public void Hide()
        {
            Panel.Children.Clear();
        }

        public void Initialize()
        {
            #region ToolbarSetup
            _fileMenu = new Menu();

            _fileMenuItem = new MenuItem();
            _fileMenuItem.Header = "File";

            _addImage = new MenuItem();
            _addImage.Click += (sender, args) => { Add?.Invoke(); };
            _addImage.Header = "Add image";

            _loadImage = new MenuItem();
            _loadImage.Click += (sender, args) => { Load?.Invoke(); };
            _loadImage.Header = "Load image";
            
            _fileNameTextBox = new TextBox();
            _fileNameTextBox.Width = 100;
            _fileNameTextBox.IsReadOnly = true;

            var fileNameLabel = new Label();
            fileNameLabel.Content = "Current image name:";

            _fileMenuItem.Items.Add(_addImage);
            _fileMenuItem.Items.Add(_loadImage);
            _fileMenu.Items.Add(_fileMenuItem);

            _toolBar = new ToolBar();
            _toolBar.Items.Add(_fileMenu);
            _toolBar.Items.Add(fileNameLabel);
            _toolBar.Items.Add(_fileNameTextBox);
            Grid.SetColumn(_toolBar, 0);
            Grid.SetRow(_toolBar, 0);
            #endregion

            #region Image setup

            _imageBox = new Image();
            _imageBox.Stretch = System.Windows.Media.Stretch.Fill;
            Grid.SetColumn(_imageBox, 0);
            Grid.SetRow(_imageBox, 1);

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
            _gridPanel.Children.Add(_imageBox);
            _gridPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            _gridPanel.VerticalAlignment = VerticalAlignment.Stretch;
            _gridPanel.Margin = new Thickness(0.2);
            #endregion
        }

        public void Show()
        {
            Panel.Children.Add(_gridPanel);
        }

        public FileData LoadImage(List<FileData> imagesInDB)
        {
            var dialog = new ChooseFileAndDateDialog();
            dialog.DataStore = imagesInDB;
            dialog.ShowDialog();
            return dialog.ChoosenFileData;
        }

        public string AddImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image file (*.jpg;*.bmp)|*.jpg;*.bmp;";
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string NameImage(string baseName)
        {
            var dialog = new ChooseFileDialog(baseName);
            dialog.ShowDialog();
            return dialog.DocumentName;
        }
    }
}
