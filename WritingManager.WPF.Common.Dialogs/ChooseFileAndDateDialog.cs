using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;
using DatabaseConnectorServiceWCF;

namespace WritingManager.WPF.Common.Dialogs
{
    public class ChooseFileAndDateDialog : Window
    {
        public FileData ChoosenFileData { get; private set; } = new FileData();

        public List<FileData> DataStore
        {
            get
            {
                return _dataStore;
            }
            set
            {
                value.Select(fd => fd.FileName).Distinct().ToList().ForEach(fileName => _fileCB.Items.Add(fileName));
                _dataStore = value;
            }
        }

        public bool WasOk { get; private set; } = false;

        private List<FileData> _dataStore { get; set; }

        private ComboBox _fileCB;
        private ComboBox _dateCB;
        private Button _acceptButton;
        private Button _cancelButton;

        public ChooseFileAndDateDialog()
        {
            Width = 300;
            Height = 200;
            MinHeight = 150;
            MinWidth = 200;
            Title = "Choose desired document name";

            _fileCB = new ComboBox();
            _dateCB = new ComboBox();

            _fileCB.SelectionChanged += (sender, e) =>
            {
                if((string)_fileCB.SelectedValue != "")
                {
                    _dateCB.Items.Clear();
                    _dataStore
                        .Where(fd => fd.FileName == (string)_fileCB.SelectedValue)
                        .Select(fd => fd.Date)
                        .ToList()
                        .ForEach(date => _dateCB.Items.Add(date));
                }
            };

            Grid.SetColumn(_fileCB, 0);
            Grid.SetColumnSpan(_fileCB, 2);
            Grid.SetRow(_fileCB, 1);

            var labelFile = new Label();
            labelFile.Content = "Choose name:";
            Grid.SetColumn(labelFile, 0);
            Grid.SetColumnSpan(labelFile, 2);
            Grid.SetRow(labelFile, 0);

            Grid.SetColumn(_dateCB, 0);
            Grid.SetColumnSpan(_dateCB, 2);
            Grid.SetRow(_dateCB, 3);

            var labelDate = new Label();
            labelDate.Content = "Choose date:";
            Grid.SetColumn(labelDate, 0);
            Grid.SetColumnSpan(labelDate, 2);
            Grid.SetRow(labelDate, 2);

            _acceptButton = new Button();
            _acceptButton.Content = "OK";
            _acceptButton.Margin = new Thickness(10, 10, 10, 10);
            _acceptButton.Click += (sender, e) => 
            {
                ChoosenFileData = new FileData()
                {
                    FileName = _fileCB.Text,
                    Date = _dateCB.SelectedValue != null ? (DateTime)_dateCB.SelectedValue : DateTime.Now
                };
                if((_fileCB.Text != null ) &&(_dateCB.SelectedValue != null))
                    WasOk = true;
                Close();
            };
            Grid.SetColumn(_acceptButton, 0);
            Grid.SetRow(_acceptButton, 4);

            _cancelButton = new Button();
            _cancelButton.Content = "Cancel";
            _cancelButton.Margin = new Thickness(10, 10, 10, 10);
            _cancelButton.Click += (sender, e) => {  Close(); };
            Grid.SetColumn(_cancelButton, 1);
            Grid.SetRow(_cancelButton, 4);
            
            var gridLayout = new Grid();
            RowDefinition rd1 = new RowDefinition();
            rd1.Height = GridLength.Auto;
            gridLayout.RowDefinitions.Add(rd1);
            RowDefinition rd2 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd2);
            RowDefinition rd3 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd3);
            RowDefinition rd4 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd4);
            RowDefinition rd5 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd5);
            ColumnDefinition cd1 = new ColumnDefinition();
            gridLayout.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            gridLayout.ColumnDefinitions.Add(cd2);

            gridLayout.Children.Add(labelDate);
            gridLayout.Children.Add(_fileCB);
            gridLayout.Children.Add(_dateCB);
            gridLayout.Children.Add(_acceptButton);
            gridLayout.Children.Add(_cancelButton);

            AddChild(gridLayout);
        }
    }
}
