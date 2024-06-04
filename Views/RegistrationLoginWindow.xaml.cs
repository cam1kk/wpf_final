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
            DataContext = new RegistrationViewModel(User);
        }
        public User User { get; set; } = new User() { Username=string.Empty};

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            User = (DataContext as RegistrationViewModel)?.User!;
            Close();
        }
    }
}
