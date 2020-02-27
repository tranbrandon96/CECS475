using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using CECS475Lab4_Calculator.Command;

namespace CECS475Lab4_Calculator.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand MySum { get; set; }
        public ICommand MyDifference { get; set; }
        public ICommand MyProduct { get; set; }

        public ICommand MyQuotient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _number1;
        public string Number1
        {
            get { return _number1; }
            set { _number1 = value; OnPropertyChanged("Number1"); }
        }

        private string _number2;
        public string Number2
        {
            get { return _number2; }
            set { _number2 = value; OnPropertyChanged("Number2"); }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged("Result"); }
        }

        public ViewModel()
        {
            MySum = new RelayCommand(sum, canexecute);
            MyDifference = new RelayCommand(difference, canexecute);
            MyProduct = new RelayCommand(product, canexecute);
            MyQuotient = new RelayCommand(quotient, canexecute);
        }

        private bool canexecute(object parameter)
        {
            if (!string.IsNullOrEmpty(Number1) && !string.IsNullOrEmpty(Number2))
            {
                return true;
            }
            return false;

        }
        private void sum(object parameter)
        {
            Result = (Convert.ToDouble(Number1) + Convert.ToDouble(Number2)).ToString();
        }

        /// <summary>
        /// Difference -> -
        /// Product -> *
        /// Quotient -> /
        /// All of these are operators that we need to use for this program. 
        /// </summary>
        /// <param name="parameter"></param>        
        private void difference(object parameter)
        {
            Result = (Convert.ToDouble(Number1) - Convert.ToDouble(Number2)).ToString();
        }
        private void product(object parameter)
        {
            Result = (Convert.ToDouble(Number1) * Convert.ToDouble(Number2)).ToString();
        }
        private void quotient(object parameter)
        {
            Result = (Convert.ToDouble(Number1) / Convert.ToDouble(Number2)).ToString();
        }

    }
}

