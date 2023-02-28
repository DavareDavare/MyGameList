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
using System.Security.Policy;
using System.Diagnostics;


namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //HttpCient -> Global gemacht da jede Funktion diesen benötigt
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Fügt Spiele der Datenbank hinzu
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

        //Updated Spiele in der Datenbank
        private async void btnPutGame_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            int id;
            Trace.WriteLine(txtIdPut.Text.Length);
            if (txtIdPut.Text.Length != 0)
            {
                id = Convert.ToInt32(txtIdPut.Text);
                string json = "{";
                if (txtGamenamePut.Text.Length != 0)
                {
                    string gamename = txtGamenamePut.Text;
                    json += "\"gamename\": \"" + gamename + "\",";
                }
                if (txtDeveloperPut.Text.Length != 0)
                {
                    string developer = txtDeveloperPut.Text;
                    json += "\"developer\": \"" + developer + "\",";
                }
                if (txtPublisherPut.Text.Length != 0)
                {
                    string publisher = txtPublisherPut.Text;
                    json += "\"publisher\": \"" + publisher + "\",";
                }
                if (txtConsolePut.Text.Length != 0)
                {
                    string console = txtConsolePut.Text;
                    json += "\"console\": \"" + console + "\",";
                }
                if (txtDescriptionPut.Text.Length != 0)
                {
                    string description = txtDescriptionPut.Text;
                    json += "\"description\": \"" + description + "\",";
                }
                if (txtPegiPut.Text.Length != 0)
                {
                    Trace.WriteLine("Cancer");
                    int pegi = Convert.ToInt32(txtPegiPut.Text);
                    Trace.WriteLine(pegi);
                    json += "\"pegi\": " + pegi + ",";
                }
                if (txtLinkPut.Text.Length != 0)
                {
                    string imagelink = txtLinkPut.Text;
                    json += "\"imagelink\": \"" + imagelink + "\",";
                }
                json = json.Remove(json.Length - 1);
                json += "}";
                Trace.WriteLine(json);

                var requestUri = new Uri("http://localhost:5000/update/" + id);
                var method = HttpMethod.Put;

                using var httpRequestMessage = new HttpRequestMessage(method, requestUri);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpRequestMessage.Content = content;
                using var response = await client.SendAsync(httpRequestMessage);

                // Read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();
                lstResponse.Items.Add(responseContent);
            }
            else
            {
                lstResponse.Items.Add("ID needed");
            }
        }

        //Holt alle Spiele der DB
        private async void btnGetAllGames_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            var responseString = await client.GetStringAsync("http://localhost:5000/getall");
            if(responseString == "[]")
            {
                lstResponse.Items.Add(responseString);
            }
            else
            {
                string[] split = responseString.Split("},{");
                if(split.Length == 1)
                {
                    lstResponse.Items.Add("[");
                    string input = split[0];
                    input = input.Replace("[", "");
                    input = input.Replace("]", "");
                    lstResponse.Items.Add(input);
                    lstResponse.Items.Add("]");
                }
                else
                {
                foreach (var item in split)
                {
                    string input = "";
                    
                    if (item == split[0])
                    {
                        lstResponse.Items.Add("[");
                        input = item;
                        input = input.Replace("[", "");
                        input = input + '}';
                    }
                    else if (item == split[split.Length - 1])
                    {

                        input = item;
                        input = input.Replace("]", "");
                        input = '{' + input;

                    }
                    else
                    {
                        input = '{' + item + '}';
                    }


                    lstResponse.Items.Add(input);
                    if (item == split[split.Length - 1])
                    {
                        lstResponse.Items.Add("]");
                    }
                }
                }
            }
        }

        //Holt Spiel über die ID
        private async void btnGetGameById_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            string id = txtID.Text;

            var responseString = await client.GetStringAsync("http://localhost:5000/get/" + id);
            lstResponse.Items.Add(responseString);
        }

        //Löscht alle Spiele in der DB
        private async void btnDeleteAllGames_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            string url = "http://localhost:5000/deleteall";
            HttpResponseMessage response = await client.DeleteAsync(url);
            lstResponse.Items.Add(response.Content.ReadAsStringAsync().Result);
            
        }

        // Löscht Spiel über die ID
        private async void btnDeleteGameById_Click(object sender, RoutedEventArgs e)
        {
            lstResponse.Items.Clear();
            string id = txtID.Text;
            string url = "http://localhost:5000/delete/" + id;
            HttpResponseMessage response = await client.DeleteAsync(url);
            lstResponse.Items.Add(response.Content.ReadAsStringAsync().Result);
        }




        //----------------Buttons Rechts-------------------
        private void btnDELETEID_Click(object sender, RoutedEventArgs e)
        {
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;
            this.gameput.Visibility = Visibility.Hidden;

            this.gamebyid.Visibility = Visibility.Visible;
            this.btnDeleteGameById.Visibility = Visibility.Visible;
        }

        private void btnDELETEALL_Click(object sender, RoutedEventArgs e)
        {
            this.btnGetAllGames.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;
            this.gameput.Visibility = Visibility.Hidden;

            this.gameall.Visibility = Visibility.Visible;
            this.btnDeleteAllGames.Visibility = Visibility.Visible;
        }

        private void btnPOST_Click(object sender, RoutedEventArgs e)
        {
            this.gameall.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameput.Visibility = Visibility.Hidden;

            this.gameadd.Visibility = Visibility.Visible;
        }

        private void btnPUT_Click(object sender, RoutedEventArgs e)
        {
            this.gameadd.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;

            this.gameput.Visibility = Visibility.Visible;

        }

        private void btnGETID_Click(object sender, RoutedEventArgs e)
        {
            this.btnDeleteGameById.Visibility = Visibility.Hidden;
            this.gameall.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;
            this.gameput.Visibility = Visibility.Hidden;

            this.gamebyid.Visibility = Visibility.Visible;
            this.btnGetGameById.Visibility = Visibility.Visible;
        }

        private void btnGETALL_Click(object sender, RoutedEventArgs e)
        {
            this.btnDeleteAllGames.Visibility = Visibility.Hidden;
            this.gamebyid.Visibility = Visibility.Hidden;
            this.gameadd.Visibility = Visibility.Hidden;
            this.gameput.Visibility = Visibility.Hidden;

            this.gameall.Visibility = Visibility.Visible;
            this.btnGetAllGames.Visibility = Visibility.Visible;
        }
    }
}
