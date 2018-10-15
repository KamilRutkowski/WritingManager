using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingManager.Module
{
    public class Command : System.Windows.Input.ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;

        public Command(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_action != null)
                _action.Invoke();
        }
    }
}
