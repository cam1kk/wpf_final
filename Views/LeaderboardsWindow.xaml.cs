using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using wpf_final.ViewModels;

namespace wpf_final.Views
{
    /// <summary>
    /// Interaction logic for Leaderboards.xaml
    /// </summary>
    public partial class LeaderboardsWindow : Window
    {
        public LeaderboardsWindow(int gameMode)
        {
            InitializeComponent();
            DataContext = new LeadersboardViewModel(gameMode);
        }
    }
}
