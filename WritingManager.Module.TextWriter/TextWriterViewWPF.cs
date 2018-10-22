using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WritingManager.Module;

namespace WritingManager.Module.TextWriter
{
    public class TextWriterViewWPF : ITextWriterViewBase<Panel>
    {
        public string Data { get; protected set; }
        public Panel Panel { protected get; set; }

        public event Action Save;

        private Grid _gridPanel;
        private ToolBar _toolBar;
        private TextBox _textBox;
        private Button _saveButton;

        public TextWriterViewWPF()
        {
            Initialize();
        }

        public void Initialize()
        {
            _saveButton = new Button();
            _saveButton.Click += (object sender, RoutedEventArgs args) => { Save?.Invoke(); };
            _saveButton.Content = "Save document";

            _toolBar = new ToolBar();
            _toolBar.Items.Add(_saveButton);
            Grid.SetColumn(_toolBar, 0);
            Grid.SetRow(_toolBar, 0);

            _textBox = new TextBox();
            _textBox.Margin = new Thickness(0.2);
            _textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            _textBox.VerticalAlignment = VerticalAlignment.Stretch;
            _textBox.TextWrapping = TextWrapping.Wrap;
            _textBox.AcceptsReturn = true;
            
            _textBox.MinWidth = 300;
            Grid.SetColumn(_textBox, 0);
            Grid.SetRow(_textBox, 1);

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

        }

        public void Show()
        {
            Panel.Children.Add(_gridPanel);
        }

        public void Hide()
        {
            Panel.Children.Clear();
        }
    }
}
