using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight; //For mvvmlight 
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;//for class Messenger
using CECS475Lab4_PreLab2.Messages;
    

namespace CECS475Lab4_PreLab2.ViewModel
{
  public class SenderViewModel: ViewModelBase     
    {
        private String _textBoxText;
        public RelayCommand OnClickCommand { get; set; }

        public string TextBoxText
        {
            get
            { return _textBoxText; }
                       
            set
            {
                _textBoxText = value;
                RaisePropertyChanged("TextBoxText");
            }
        }

        public SenderViewModel()
        {
            OnClickCommand = new RelayCommand(OnClickCommandAction, null);
        }

        private void OnClickCommandAction()
        {
            var viewModelMessage = new ViewModelMessage()
            {
                Text = TextBoxText
            };
            Messenger.Default.Send(viewModelMessage);
        }

    }
}
