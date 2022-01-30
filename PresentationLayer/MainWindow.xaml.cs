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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBl;

        public MainWindow(IBL ibl)
        {
            myBl = ibl;
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            new EntrenceWindow().Show();
            base.OnClosing(e);
        }

        private void btnShowDrones_Click(object sender, RoutedEventArgs e)
        {
            DronesListWindow dw =  new DronesListWindow(myBl);
            dw.Owner = this;
            dw.Show();
        }

        private void btnShowStations_Click(object sender, RoutedEventArgs e)
        {
            StationsListWindow sw = new StationsListWindow(myBl);
            sw.Owner = this;
            sw.Show();
        }

        private void btnShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomersListWindow cw = new CustomersListWindow(myBl);
            cw.Owner = this;
            cw.Show();
        }

        private void btnShowParcels_Click(object sender, RoutedEventArgs e)
        {
            ParcelsListWindow pw = new ParcelsListWindow(myBl);
            pw.Owner = this;
            pw.Show();
        }
    }
}
