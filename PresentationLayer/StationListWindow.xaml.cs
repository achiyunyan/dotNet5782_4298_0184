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
    public partial class StationListWindow : Window
    {
        private bool exit = false;
        private BlApi.IBL ibl;

        public StationListWindow(BlApi.IBL bL)
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
            new MainWindow().Show();
            exit = true;
            Close();
        }

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstvStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstvStations.ItemsSource = ibl.GetStationsList();
        }

        private void btnGroupBySlotsNumber_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from Station in (IEnumerable<ListStation>)lstvStations.ItemsSource
                             group Station by Station.FreeChargeSlots;
            lstvStations.ItemsSource = from list in groupsList
                                       from Station in list
                                       select Station;
        }

        private void btnGroupByExistingSlots_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from Station in (IEnumerable<ListStation>)lstvStations.ItemsSource
                             group Station by (Station.FreeChargeSlots>0);
            lstvStations.ItemsSource = from list in groupsList
                                     from Station in list
                                     select Station;
        }
    }
}
