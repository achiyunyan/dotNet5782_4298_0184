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
using System.Windows.Shapes;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationsListWindow : Window
    {
        private bool exit = false;
        private BlApi.IBL ibl;

        public StationsListWindow(BlApi.IBL bL)
        {
            InitializeComponent();
            ibl = bL;
            lstvStations.ItemsSource = ibl.GetStationsList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        public void Refresh()
        {
            lstvStations.ItemsSource = ibl.GetStationsList();
        }

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(ibl).Show();
        }

        private void lstvStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstvStations.Items.Count > 0)
            {
                StationWindow sw = new StationWindow(ibl, (BO.ListStation)lstvStations.SelectedItem);
                sw.Owner = this;
                sw.Show();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnGroupBySlotsNumber_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from Station in (IEnumerable<ListStation>)lstvStations.ItemsSource
                             group Station by Station.FreeChargeSlots;
            lstvStations.ItemsSource = from list in groupsList
                                       from Station in list
                                       select Station;
        }

        private void btnGroupByFreeSlots_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from Station in (IEnumerable<ListStation>)lstvStations.ItemsSource
                             group Station by (Station.FreeChargeSlots > 0);
            lstvStations.ItemsSource = from list in groupsList
                                       from Station in list
                                       select Station;
        }
    }
}
