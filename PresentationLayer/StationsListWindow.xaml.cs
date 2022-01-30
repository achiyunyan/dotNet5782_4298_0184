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

        /// <summary>
        /// Ctor for StationList window
        /// </summary>
        /// <param name="bL"></param>
        public StationsListWindow(BlApi.IBL bL)
        {
            InitializeComponent();
            ibl = bL;
            lstvStations.ItemsSource = ibl.GetStationsList();
        }

        /// <summary>
        /// Let only allowed exit to happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        /// <summary>
        /// 'Close' button handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        /// <summary>
        /// Reset the itemSource to the current stationList 
        /// </summary>
        public void Refresh()
        {
            lstvStations.ItemsSource = ibl.GetStationsList();
        }

        /// <summary>
        /// Open stationWindow with add option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {
            StationWindow sw =  new StationWindow(ibl);
            sw.Owner = this;
            sw.Show();
        }

        /// <summary>
        /// Specific Station was selected. open the station window in order to visualize the station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstvStations.Items.Count > 0)
            {
                StationWindow sw = new StationWindow(ibl, (BO.ListStation)lstvStations.SelectedItem);
                sw.Owner = this;
                sw.Show();
            }
        }

        /// <summary>
        /// Activate the refresh function after the refresh button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Group the Stations' list by the number of slots available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySlotsNumber_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from Station in (IEnumerable<ListStation>)lstvStations.ItemsSource
                             group Station by Station.FreeChargeSlots;
            lstvStations.ItemsSource = from list in groupsList
                                       from Station in list
                                       select Station;
        }

        /// <summary>
        /// Group the Stations' list by the existance slots available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
