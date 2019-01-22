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
    public class ChooseStringDialog : Window
    {
        public string ChoosenString { get; private set; } = "";

        public List<string> DataStore
        {
            get
            {
                return _dataStore;
            }
            set
            {
                _dataStore = value;
            }
        }

        private List<string> _dataStore { get; set; }

        private ComboBox _stringCB;
        private Button _acceptButton;
        private Button _cancelButton;

        public ChooseStringDialog()
        {
            Width = 300;
            Height = 200;
            MinHeight = 150;
            MinWidth = 200;
            Title = "Choose desired document name";

            _stringCB = new ComboBox();

            Grid.SetColumn(_stringCB, 0);
            Grid.SetColumnSpan(_stringCB, 2);
            Grid.SetRow(_stringCB, 1);

            var labelFile = new Label();
            labelFile.Content = "Choose name:";
            Grid.SetColumn(labelFile, 0);
            Grid.SetColumnSpan(labelFile, 2);
            Grid.SetRow(labelFile, 0);
            

            _acceptButton = new Button();
            _acceptButton.Content = "OK";
            _acceptButton.Margin = new Thickness(10, 10, 10, 10);
            _acceptButton.Click += (sender, e) => { ChoosenString = _stringCB.Text; Close(); };
            Grid.SetColumn(_acceptButton, 0);
            Grid.SetRow(_acceptButton, 4);

            _cancelButton = new Button();
            _cancelButton.Content = "Cancel";
            _cancelButton.Margin = new Thickness(10, 10, 10, 10);
            _cancelButton.Click += (sender, e) => { Close(); };
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

            gridLayout.Children.Add(_stringCB);
            gridLayout.Children.Add(_acceptButton);
            gridLayout.Children.Add(_cancelButton);

            AddChild(gridLayout);
        }
    }
}
