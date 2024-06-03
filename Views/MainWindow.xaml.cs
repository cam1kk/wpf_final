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
using wpf_final.ViewModels;

namespace wpf_final.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationLoginWindow registrationloginWindow = new RegistrationLoginWindow();
            registrationloginWindow.Show();
        }

        private void LeaderboardsButton_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardsWindow leaderboardsWindow = new LeaderboardsWindow();
            leaderboardsWindow.Show();
        }
        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccountWindow personalAccountWindow = new PersonalAccountWindow();
            personalAccountWindow.Show();
        }
        
    }
}
