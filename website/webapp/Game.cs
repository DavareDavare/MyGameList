namespace MyApplication
{
    public class Game
    {
        public int _id { get; set; }
        public string gamename { get; set; }
        public string developer { get; set; }
        public string publisher { get; set; }
        public string console { get; set; }
        public string description { get; set; }
        public int pegi { get; set; }
        public string imagelink { get; set; }

        Game(int id, string gamename, string developer, string publisher, string console, string description, int pegi, string imagelink)
        {
            _id = id;
            this.gamename = gamename;
            this.developer = developer;
            this.publisher = publisher;
            this.console = console;
            this.description = description;
            this.pegi = pegi;
            this.imagelink = imagelink;
        }
    }
}
