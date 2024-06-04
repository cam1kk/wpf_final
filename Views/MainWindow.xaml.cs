using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf_final.Models;
using wpf_final.ViewModels;

namespace wpf_final.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(_gameMode);
            fiveButton.IsEnabled = false;

            accountButton.Height = 0;
            accountButton.Width = 0;
            accountButton.Margin = new Thickness(0, 0, 0, 0);
        }

        public User User { get; set; }
        private int _gameMode = 5;

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationLoginWindow registrationloginWindow = new RegistrationLoginWindow();
            registrationloginWindow.ShowDialog();
            if (registrationloginWindow.User.Username != string.Empty)
            {
                User = registrationloginWindow.User;
                (DataContext as MainViewModel).User = User;
                accountButton.Height = 40;
                accountButton.Width = 40;
                accountButton.Margin = new Thickness(5, 5, 5, 5);
                registrationButton.Height = 0;
                registrationButton.Width = 0;
                registrationButton.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private void LeaderboardsButton_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardsWindow leaderboardsWindow = new LeaderboardsWindow(_gameMode);
            leaderboardsWindow.ShowDialog();
        }
        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccountWindow personalAccountWindow = new PersonalAccountWindow(User, _gameMode);
            personalAccountWindow.ShowDialog();
            if (personalAccountWindow.isLoggedOut)
            {
                accountButton.Height = 0;
                accountButton.Width = 0;
                accountButton.Margin = new Thickness(0, 0, 0, 0);
                registrationButton.Height = 40;
                registrationButton.Width = 40;
                registrationButton.Margin = new Thickness(5, 5, 5, 5);
            }
        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = false;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = true;
            GuessTextBox.Text = string.Empty;
            _gameMode = 3;
        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = false;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = true;
            GuessTextBox.Text = string.Empty;
            _gameMode = 5;
        }

        private void sevenButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = false;
            nineButton.IsEnabled = true;
            GuessTextBox.Text = string.Empty;
            _gameMode = 7;
        }

        private void nineButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = false;
            GuessTextBox.Text = string.Empty;
            _gameMode = 9;
        }
    }
}
