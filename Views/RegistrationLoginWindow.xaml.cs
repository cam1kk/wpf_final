using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpf_final.Encoder;
using wpf_final.Models;
using wpf_final.ViewModels;

namespace wpf_final.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationLoginWindow : Window
    {
        public RegistrationLoginWindow()
        {
            InitializeComponent();
        }
        public User User { get; set; } = new User() { Username=string.Empty};

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationEmail.Text != string.Empty && RegistrationUsername.Text != string.Empty && RegistrationPassword.Text != string.Empty)
            {
                var database = new UsersDatabase();
                if (database.GetUsers().All(u => u.Email != RegistrationEmail.Text))
                {
                    if (database.GetUsers().All(u => u.Username != RegistrationUsername.Text))
                    {
                        string? passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
                        Regex regex = new Regex(passwordPattern);
                        if (regex.IsMatch(RegistrationPassword.Text))
                        {
                            User = new User { Email = RegistrationEmail.Text, Password = EncoderSHA256.ToSHA256(RegistrationPassword.Text), Username = RegistrationUsername.Text };
                            database.AddUser(User);
                            Close();
                        }
                        else
                        {
                            RegisterError.Text = "Password is too simple!";
                        }
                    }
                    else
                    {
                        RegisterError.Text = "Already registered username";
                    }
                }
                else
                {
                    RegisterError.Text = "Already registered email";
                }
            }
            else
            {
                RegisterError.Text = "Fill every field!";
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginUsername.Text != string.Empty && LoginPassword.Text != string.Empty)
            {
                var database = new UsersDatabase();
                if (database.GetUsers().Any(u => u.Username == LoginUsername.Text))
                {
                    if (database.GetUsers().Where(u => u.Username == LoginUsername.Text).ToList()[0].Password == EncoderSHA256.ToSHA256(LoginPassword.Text))
                    {
                        User = database.GetUsers().Where(u => u.Username == LoginUsername.Text).ToList()[0];
                        Close();
                    }
                    else
                    {
                        LoginError.Text = "Wrong password!";
                    }
                }
                else
                {
                    LoginError.Text = "Unknown username";
                }
            }
            else
            {
                LoginError.Text = "Fill every field!";
            }
        }
    }
}
