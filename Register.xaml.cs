using Microsoft.Azure.Cosmos;
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

namespace JasenrekisteriNoSQL
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    /// 

    

    public partial class Register : Page
    {

        public Register()
        {
            InitializeComponent();
        }

        private async void PageGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Lataa tiedot tietokannasta joka kerta kun sivu avataan
            CosmosClient client = new CosmosClient(MainWindow.cosmosUrl, MainWindow.cosmosKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync("Register");

            Container container = await database.CreateContainerIfNotExistsAsync("Members",
                "/partitionKey");

            var sqlQueryText = "SELECT * FROM c";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Member> queryResultSetIterator = container.GetItemQueryIterator<Member>(queryDefinition);

            List<Member> members = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Member> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Member m in currentResultSet)
                {
                    members.Add(m);
                }
            }
            RegisterDG.ItemsSource = members;
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = RegisterDG.SelectedItem as Member;

            if (item is not null)
            {
                CosmosClient client = new CosmosClient(MainWindow.cosmosUrl, MainWindow.cosmosKey);
                Database database = await client.CreateDatabaseIfNotExistsAsync("Register");

                Container container = await database.CreateContainerIfNotExistsAsync("Members",
                    "/partitionKey");


                ItemResponse<Member> response = await container.DeleteItemAsync<Member>(item.Id, PartitionKey.None);

                MessageBox.Show("Jäsen poistettu!");

                PageGrid_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Valitse jäsen!");
            }
        }

        public void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = RegisterDG.SelectedItem as Member;
            if (item is not null)
            {
                AddMember.ActiveId = item.Id;
                MainWindow.addMember.EtunimiTxB.Text = item.Etunimi;
                MainWindow.addMember.SukunimiTxB.Text = item.Sukunimi;
                MainWindow.addMember.OsoiteTxB.Text = item.Osoite;
                MainWindow.addMember.PostinumeroTxB.Text = item.Postinumero;
                MainWindow.addMember.PuhelinnumeroTxB.Text = item.Puhelin;
                MainWindow.addMember.SahkopostiTxB.Text = item.Sahkoposti;
                MainWindow.addMember.AlkuPvmDP.Text = item.JasenyydenAlkuPvm;
                AddMember.Update = true;
                this.NavigationService.Navigate(MainWindow.addMember);
            }
            else
            {
                MessageBox.Show("Valitse jäsen!");
            }
        }
    }
}
