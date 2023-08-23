using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class AddMember : Page
    {

        public static bool Update { get; set; } = false;

        public static string? ActiveId { get; set; }

        public AddMember()
        {
            InitializeComponent();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(EtunimiTxB.Text) || string.IsNullOrEmpty(SukunimiTxB.Text) 
                || string.IsNullOrEmpty(OsoiteTxB.Text) || string.IsNullOrEmpty(PostinumeroTxB.Text)
                || string.IsNullOrEmpty(PuhelinnumeroTxB.Text) || string.IsNullOrEmpty(SahkopostiTxB.Text))
            {
                MessageBox.Show("Ei riittävästi tietoja!");
            }
            else
            {

                CosmosClient client = new CosmosClient(MainWindow.cosmosUrl, MainWindow.cosmosKey);
                Database database = await client.CreateDatabaseIfNotExistsAsync("Register");

                Container container = await database.CreateContainerIfNotExistsAsync("Members", "/partitionKey");

                if (Update) //Jos tultiin Muokkaa napin kautta sivulle..
                {
                    ItemResponse<Member> response = await container.ReadItemAsync<Member>(ActiveId, PartitionKey.None);
                    var itembody = response.Resource;

                    itembody.Etunimi = EtunimiTxB.Text;
                    itembody.Sukunimi = SukunimiTxB.Text;
                    itembody.Osoite = OsoiteTxB.Text;
                    itembody.Postinumero = PostinumeroTxB.Text;
                    itembody.Puhelin = PuhelinnumeroTxB.Text;
                    itembody.Sahkoposti = SahkopostiTxB.Text;
                    itembody.JasenyydenAlkuPvm = AlkuPvmDP.Text;

                    try
                    {
                        response = await container.ReplaceItemAsync<Member>(itembody, ActiveId, PartitionKey.None);
                        MessageBox.Show("Jäsen Muokattu!");
                        ResetTextBoxes();
                    }
                    catch
                    {
                        MessageBox.Show("Jäsenen muokkaaminen epäonnistui!");
                    }

                    
                }
                else    //Jos tultiin Lisää napin kautta..
                {
                    var sqlQueryText = "SELECT * FROM c";

                    QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                    FeedIterator<Member> queryResultSetIterator = container.GetItemQueryIterator<Member>(queryDefinition);

                    //Haetaan ja lasketaan uusi Id tietokannasta
                    int count = 0;
                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<Member> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (Member mm in currentResultSet)
                        {
                            count++;
                        }
                    }
                    count += 1;

                    Member m = new Member(count.ToString(), EtunimiTxB.Text, SukunimiTxB.Text, OsoiteTxB.Text,
                                PostinumeroTxB.Text, PuhelinnumeroTxB.Text, SahkopostiTxB.Text, AlkuPvmDP.Text);

                    try
                    {
                        ItemResponse<Member> response = await container.CreateItemAsync(m);
                        MessageBox.Show("Jäsen lisätty!");
                        ResetTextBoxes();
                    }
                    catch
                    {
                        MessageBox.Show("Jäsenen lisääminen epäonnistui!");
                    }
                }
                Update = false;
            }
        }

        private void ResetTextBoxes()
        {
            EtunimiTxB.Text = "";
            SukunimiTxB.Text = "";
            OsoiteTxB.Text = "";
            PostinumeroTxB.Text = "";
            SahkopostiTxB.Text = "";
            PuhelinnumeroTxB.Text = "";
            AlkuPvmDP.Text = "";
        }
    }
}
