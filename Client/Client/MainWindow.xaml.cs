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
            lstResponse.Items.Clear();
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
            lstResponse.Items.Add(response.Content.ReadAsStringAsync().Result);
        }
        private void btnPutGame_Click(object sender, RoutedEventArgs e)
        {
            string gamename = txtGamename.Text;
            string developer = txtDeveloper.Text;
            string publisher = txtPublisher.Text;
            string console = txtConsole.Text;
            string description = txtDescription.Text;
            int pegi = Convert.ToInt32(txtPegi.Text);
            string imagelink = txtLink.Text;
        }

        private async void btnGetAllGames_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            var responseString = await client.GetStringAsync("http://localhost:5000/getall");
            string[] split = responseString.Split("},{");
            foreach( var item in split )
            {
                lstResponse.Items.Add(item);
            }
        }
        private void btnGetGameById_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteAllGames_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteGameById_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDELETEID_Click(object sender, RoutedEventArgs e)
        {
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;

            this.gamebyid.Visibility = Visibility.Visible;
            this.btnDeleteGameById.Visibility = Visibility.Visible;
        }

        private void btnDELETEALL_Click(object sender, RoutedEventArgs e)
        {
            this.btnGetAllGames.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;

            this.gameall.Visibility = Visibility.Visible;
            this.btnDeleteAllGames.Visibility = Visibility.Visible;
        }

        private void btnPOST_Click(object sender, RoutedEventArgs e)
        {
            this.btnPutGame.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;

            this.gameadd.Visibility = Visibility.Visible;
            this.btnAdd.Visibility = Visibility.Visible;
        }

        private void btnPUT_Click(object sender, RoutedEventArgs e)
        {
            this.btnAdd.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;

            this.gameadd.Visibility = Visibility.Visible;
            this.btnPutGame.Visibility = Visibility.Visible;

        }

        private void btnGETID_Click(object sender, RoutedEventArgs e)
        {
            this.btnDeleteGameById.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;

            this.gamebyid.Visibility = Visibility.Visible;
            this.btnGetGameById.Visibility = Visibility.Visible;
        }

        private void btnGETALL_Click(object sender, RoutedEventArgs e)
        {
            this.btnDeleteAllGames.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;

            this.gameall.Visibility = Visibility.Visible;
            this.btnGetAllGames.Visibility = Visibility.Visible;
        }
    }
}
