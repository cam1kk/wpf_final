using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_final.Commands;
using wpf_final.Encoder;
using wpf_final.Models;
using wpf_final.Views;

namespace wpf_final.ViewModels
{
    public class RegistrationViewModel : ViewModel
    {
        private UsersDatabase _database;
        public User User { get; set; }

        public string RUsername { get; set; } = string.Empty;
        public string REmail { get; set; } = string.Empty;
        public string RPassword { get; set; } = string.Empty;
        public string LUsername { get; set; } = string.Empty;
        public string LPassword { get; set; } = string.Empty;
        private string _rError;
        private string _lError;
        private string _close;
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public string Close
        {
            get => _close;
            set
            {
                _close = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Close)));
            }
        }

        public string RError
        {
            get => _rError;
            set
            {
                _rError = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RError)));
            }
        }
        public string LError
        {
            get => _lError;
            set
            {
                _lError = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LError)));
            }
        }
        public RegistrationViewModel(User user)
        {
            _database = new UsersDatabase();
            RegisterCommand = new DelegateCommand(Register);
            LoginCommand = new DelegateCommand(Login);
            User = user;
        }
        public void Register()
        {
            if (REmail != string.Empty && RUsername != string.Empty && RPassword != string.Empty)
            {
                var database = new UsersDatabase();
                if (database.GetUsers().All(u => u.Email != REmail))
                {
                    if (database.GetUsers().All(u => u.Username != RUsername))
                    {
                        string? passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
                        Regex regex = new Regex(passwordPattern);
                        if (regex.IsMatch(RPassword))
                        {
                            User = new User { Email = REmail, Password = EncoderSHA256.ToSHA256(RPassword), Username = RUsername };
                            database.AddUser(User);
                            Close = "Close";
                        }
                        else
                        {
                            RError = "Password is too simple!";
                        }
                    }
                    else
                    {
                        RError = "Already registered username";
                    }
                }
                else
                {
                    RError = "Already registered email";
                }
            }
            else
            {
                RError = "Fill every field!";
            }
        }
        public void Login()
        {
            if (LPassword != string.Empty && LUsername != string.Empty)
            {
                var database = new UsersDatabase();
                if (database.GetUsers().Any(u => u.Username == LUsername))
                {
                    if (database.GetUsers().Where(u => u.Username == LUsername).ToList()[0].Password == EncoderSHA256.ToSHA256(LPassword))
                    {
                        User = database.GetUsers().Where(u => u.Username == LUsername).ToList()[0];
                        Close = "Close";
                    }
                    else
                    {
                        LError = "Wrong password!";
                    }
                }
                else
                {
                    LError = "Unknown username";
                }
            }
            else
            {
                LError = "Fill every field!";
            }
        }
    }
}
