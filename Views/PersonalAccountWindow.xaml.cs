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
using System.Windows.Shapes;
using wpf_final.Models;

namespace wpf_final.Views
{
    /// <summary>
    /// Interaction logic for PersonalAccountWindow.xaml
    /// </summary>
    public partial class PersonalAccountWindow : Window
    {
        public PersonalAccountWindow(User User, int gameMode)
        {
            InitializeComponent();
            Username.Text = User.Username;
            Score.Text = $"Score {((User.ModeScore[gameMode] != 0) ? User.ModeScore[gameMode] : "---")}";
        }

        public bool isLoggedOut { get; set; } = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isLoggedOut = true;
            Close();
        }
    }
}
