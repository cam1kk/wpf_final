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
        public MainWindow(User? user = null)
        {
            InitializeComponent();
            if (user == null)
            {
                user = new User() { Username = string.Empty };
            }
            DataContext = new MainViewModel(5, user);
            fiveButton.IsEnabled = false;
        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = false;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = true;
        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = false;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = true;
        }

        private void sevenButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = false;
            nineButton.IsEnabled = true;
        }

        private void nineButton_Click(object sender, RoutedEventArgs e)
        {
            threeButton.IsEnabled = true;
            fiveButton.IsEnabled = true;
            sevenButton.IsEnabled = true;
            nineButton.IsEnabled = false;
        }
    }
}
