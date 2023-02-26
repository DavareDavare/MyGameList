using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Game
    {
        public int id { get; set; }
        public string gamename { get; set; }
        public string developer { get; set; }
        public string publisher { get; set; }
        public string console { get; set; }
        public string description { get; set; }
        public int pegi { get; set; }

        public string imagelink { get; set; }

        public Game(string gamename, string developer, string publisher, string console, string desription, int pegi, string imagelink)
        {
            this.gamename = gamename;
            this.developer = developer;
            this.publisher = publisher;
            this.console = console;
            this.description = desription;
            this.pegi = pegi;
            this.imagelink = imagelink;
        }
    }
}
