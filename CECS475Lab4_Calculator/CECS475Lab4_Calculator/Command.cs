﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CECS475Lab4_Calculator.Command
{
    public class RelayCommand : ICommand
    {

        Action<object> _execute;
        Func<object, bool> _canexecute;
        public RelayCommand(Action<object> execute, Func<object, bool> canexecute)
        {
            _execute = execute;
            _canexecute = canexecute;

        }
        public bool CanExecute(object parameter)
        {
            if (_canexecute != null)
            {
                return _canexecute(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}