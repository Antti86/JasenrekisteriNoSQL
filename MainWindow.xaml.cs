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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Register register = new();
        public static AddMember addMember = new();

        public static readonly string cosmosUrl = "Lisää tämän tekstin tilalle cosmos tietokannan Url osoite";

        public static readonly string cosmosKey = "Lisää tämän tekstin tilalle cosmos tietokannan pääavain";
        public MainWindow()
        {
            InitializeComponent();
            Page.NavigationService.Navigate(register);
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            Page.NavigationService.Navigate(addMember);
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            Page.NavigationService.Navigate(register);
        }

    }
}
