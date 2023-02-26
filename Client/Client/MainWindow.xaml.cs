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
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string gamename = txtGamename.Text;
            string developer = txtDeveloper.Text;
            string publisher = txtPublisher.Text;
            string console = txtConsole.Text;
            string description = txtDescription.Text;
            int pegi = Convert.ToInt32(txtPegi.Text);
            string imagelink = txtLink.Text;

            Game game = new Game(gamename, developer, publisher, console, description, pegi, imagelink);
            string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(game);

            var response = await client.PostAsync("http://localhost:5000/add", new StringContent(jsonstring, Encoding.UTF8, "application/json"));

            lstResponse.Items.Add(response);
        }

        private void lstNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
