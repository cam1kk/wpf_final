using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_final.Models;

namespace wpf_final.ViewModels
{
    public class LeadersboardViewModel : ViewModel
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        public LeadersboardViewModel(int gameMode)
        {
            var database = new UsersDatabase();
            var users = database.GetUsers();
            var players = new List<Player>();
            foreach (var user in users)
            {
                if (user.ModeScore[gameMode] > 0)
                {
                    players.Add(new Player() { Score = user.ModeScore[gameMode], Username = user.Username });
                }
            }
            players = players.ToList().OrderByDescending(p => p.Score).ToList();
            foreach (var item in players)
            {
                Players.Add(item);
            }
        }
    }
}
