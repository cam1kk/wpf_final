using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_final.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Dictionary<int, int> ModeScore { get; set; }
        public User()
        {
            ModeScore = new Dictionary<int, int>();
            ModeScore[3] = 0;
            ModeScore[5] = 0;
            ModeScore[7] = 0;
            ModeScore[9] = 0;
        }
    }
}
