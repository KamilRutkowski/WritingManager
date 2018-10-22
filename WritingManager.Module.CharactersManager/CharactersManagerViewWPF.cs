using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WritingManager.Module.CharactersManager
{
    public class CharactersManagerViewWPF : ICharactersManagerViewBase<Panel>
    {
        public Panel Panel { protected get; set; }
        private Button _test;
        
        public CharactersManagerViewWPF()
        {
            Initialize();
        }

        public void Initialize()
        {
            _test = new Button();
            _test.Content = "Działa";
        }

        public void Show()
        {
            Panel.Children.Add(_test);
        }

        public void Hide()
        {
            Panel.Children.Clear();
        }
    }
}
