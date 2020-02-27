using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CECS475Lab4_FitnessMembership.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CECS475Lab4_FitnessMembership.ViewModel
{
    /// <summary>
    /// The VM for modifying or removing users.
    /// </summary>
    public class ChangeViewModel : ViewModelBase
    {
        /// <summary>
        /// The currently entered first name in the change window.
        /// </summary>
        private string enteredFName;

        /// <summary>
        /// The currently entered last name in the change window.
        /// </summary>
        private string enteredLName;

        /// <summary>
        /// The currently entered email in the change window.
        /// </summary>
        private string enteredEmail;

        /// <summary>
        /// Initializes a new instance of the ChangeViewModel class.
        /// </summary>
        public ChangeViewModel()
        {
            // (Missing)
            // Two Commands -> UpdateCommand, DeleteCommand
            UpdateCommand = new RelayCommand<IClosable>(this.UpdateMethod); 
            DeleteCommand = new RelayCommand<IClosable>(this.DeleteMethod); 
            Messenger.Default.Register<Member>(this, GetSelected);
        }

        /// <summary>
        /// The command that triggers saving the filled out member data.
        /// </summary>
        public ICommand UpdateCommand { get; private set; }

        /// <summary>
        /// The command that triggers removing the previously selected user.
        /// </summary>
        public ICommand DeleteCommand { get; private set; }

        /// <summary>
        /// Sends a valid member to the main VM to replace at the selected index with, then closes the change window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void UpdateMethod(IClosable window)
        {
            try
            {
                // (Missing) -> Sends a valid member
                Messenger.Default.Send(new MessageMember(EnteredFName, EnteredLName, EnteredEmail, "Update"));
                window.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
            }
            catch (NullReferenceException) // (Missing) 
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException) // (Missing)
            {
                MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
            }
        }

        /// <summary>
        /// Sends out a message to initiate closing the change window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void DeleteMethod(IClosable window)
        {
            if (window != null)
            {
                // (Missing) -> Sends out a message
                Messenger.Default.Send(new NotificationMessage("Delete"));
                window.Close();
            }
        }

        /// <summary>
        /// Receives a member from the main VM to auto-fill the change box with the currently selected member.
        /// </summary>
        /// <param name="m">The member data to fill in.</param>
        public void GetSelected(Member m)
        {
            // (Missing) 
            // Receives the member from the main. Initialize it to the local variable 
            EnteredFName = m.FirstName;
            EnteredLName = m.LastName;
            EnteredEmail = m.Email; 
        }

        /// <summary>
        /// The currently entered first name in the change window.
        /// </summary>
        public string EnteredFName
        {
            get
            {
                return enteredFName;
            }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }
        /// <summary>
        /// The currently entered last name in the change window.
        /// </summary>
        public string EnteredLName
        {
            get
            {
                return enteredLName;
            }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }
        /// <summary>
        /// The currently entered email name in the change window.
        /// </summary>
        public string EnteredEmail
        {
            get
            {
                return enteredEmail;
            }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }

    }
}

