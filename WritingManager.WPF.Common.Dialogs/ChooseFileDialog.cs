using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;

namespace WritingManager.WPF.Common.Dialogs
{
    public class ChooseFileDialog: Window
    {
        public string DocumentName { get; private set; } = "";

        private TextBox _textBox;
        private Button _acceptButton;
        private Button _cancelButton;

        public ChooseFileDialog()
        {
            Width = 300;
            Height = 200;
            MinHeight = 150;
            MinWidth = 200;
            Title = "Choose desired document name";

            _textBox = new TextBox();
            Grid.SetColumn(_textBox, 0);
            Grid.SetColumnSpan(_textBox, 2);
            Grid.SetRow(_textBox, 1);

            var label = new Label();
            label.Content = "Choose document name:";
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            Grid.SetRow(label, 0);

            _acceptButton = new Button();
            _acceptButton.Content = "OK";
            _acceptButton.Margin = new Thickness(10, 10, 10, 10);
            _acceptButton.Click += (sender, e) => { DocumentName = _textBox.Text; Close(); };
            Grid.SetColumn(_acceptButton, 0);
            Grid.SetRow(_acceptButton, 2);

            _cancelButton = new Button();
            _cancelButton.Content = "Cancel";
            _cancelButton.Margin = new Thickness(10, 10, 10, 10);
            _cancelButton.Click += (sender, e) => { DocumentName = ""; Close(); };
            Grid.SetColumn(_cancelButton, 1);
            Grid.SetRow(_cancelButton, 2);

            //Closed += (sender, e) => { DocumentName = ""; Close(); };

            var gridLayout = new Grid();
            RowDefinition rd1 = new RowDefinition();
            rd1.Height = GridLength.Auto;
            gridLayout.RowDefinitions.Add(rd1);
            RowDefinition rd2 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd2);
            RowDefinition rd3 = new RowDefinition();
            gridLayout.RowDefinitions.Add(rd3);
            ColumnDefinition cd1 = new ColumnDefinition();
            gridLayout.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            gridLayout.ColumnDefinitions.Add(cd2);

            gridLayout.Children.Add(label);
            gridLayout.Children.Add(_textBox);
            gridLayout.Children.Add(_acceptButton);
            gridLayout.Children.Add(_cancelButton);

            AddChild(gridLayout);
        }
    }
}
