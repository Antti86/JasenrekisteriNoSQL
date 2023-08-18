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
        readonly Register register = new();
        readonly AddMember addMember = new();
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
