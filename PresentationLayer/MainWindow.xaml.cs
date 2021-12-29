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
using BlApi;
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBl = BlFactory.GetBl();

        public MainWindow()
        {
            InitializeComponent();
        }

         private void btnShowDrones_Click(object sender, RoutedEventArgs e)
        {
            new DronesListWindow(myBl).Show();
            Close();
        }

        private void btnShowStations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowCustomers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowParcels_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsListWindow(myBl).Show();
            Close();
        }
    }
}
