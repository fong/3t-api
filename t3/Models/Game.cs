using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace t3.Models
{
    public class Game
    {
        public string gameID { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public string board { get; set; }
        public int watchers { get; set; }
        public int turn { get; set; }
        public long p1_timestamp { get; set; }
        public long p2_timestamp { get; set; }

    }
}
