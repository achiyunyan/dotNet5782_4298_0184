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
    /// Interaction logic for DisplaydronesWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        BlApi.IBL ibl;
        bool exit = false;//stops the window from being closed by x

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="myBl"></param>
        public DronesListWindow(BlApi.IBL myBl)
        {
            ibl = myBl;
            InitializeComponent();

            this.comboMaxWeight.Items.Add("");
            foreach (var x in Enum.GetValues(typeof(WeightCategory)))
            {
                this.comboMaxWeight.Items.Add(x);
            }

            this.comboStatus.Items.Add("");
            foreach (var x in Enum.GetValues(typeof(DroneState)))
            {
                this.comboStatus.Items.Add(x);
            }

            this.lstvDrones.ItemsSource = ibl.GetDronesList();
        }

        #region View Actions
        /// <summary>
        /// filters the list by status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboStatus.SelectedItem.ToString() == "")
            {
                if (comboMaxWeight.SelectedItem == null || comboMaxWeight.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList();
                else
                {
                    WeightCategory? maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.WeightCategory == maxWeight));
                }
            }
            else
            {
                DroneState state = (DroneState)comboStatus.SelectedItem;
                if (comboMaxWeight.SelectedItem == null || comboMaxWeight.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.State == state);
                else
                {
                    WeightCategory? maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.WeightCategory == maxWeight) && (drone.State == state));
                }
            }
        }

        /// <summary>
        /// filters the list by max weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboMaxWeight.SelectedItem.ToString() == "")
            {
                if (comboStatus.SelectedItem == null || comboStatus.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList();
                else
                {
                    DroneState? status = (DroneState)comboStatus.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.State == status));
                }
            }
            else
            {
                WeightCategory maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
                if (comboStatus.SelectedItem == null || comboStatus.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.WeightCategory == maxWeight);
                else
                {
                    DroneState? state = (DroneState)comboStatus.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.WeightCategory == maxWeight) && (drone.State == state));
                }
            }

        }

        /// <summary>
        /// groups the list by state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupByState_Click(object sender, RoutedEventArgs e)
        {
            var groupsList = from drone in (IEnumerable<ListDrone>)lstvDrones.ItemsSource
                             group drone by drone.State;
            lstvDrones.ItemsSource = from list in groupsList
                                     from drone in list
                                     select drone;
        }

        /// <summary>
        /// updates the list according to the filters
        /// </summary>
        public void Refresh()
        {

            if (comboStatus.SelectedItem == null || comboStatus.SelectedItem.ToString() == "")
            {
                if (comboMaxWeight.SelectedItem == null || comboMaxWeight.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList();
                else
                {
                    WeightCategory? maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.WeightCategory == maxWeight));
                }
            }
            else
            {
                DroneState state = (DroneState)comboStatus.SelectedItem;
                if (comboMaxWeight.SelectedItem == null || comboMaxWeight.SelectedItem.ToString() == "")
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.State == state);
                else
                {
                    WeightCategory? maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
                    this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => (drone.WeightCategory == maxWeight) && (drone.State == state));
                }
            }
        }
        #endregion

        #region Other Actions
        /// <summary>
        /// opens a drone actions window of the selected drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openActionsWindow(object sender, MouseButtonEventArgs e)
        {
            if (lstvDrones.Items.Count > 0)
            {
                DroneWindow dw = new DroneWindow((ListDrone)lstvDrones.SelectedItem, ibl);
                dw.Owner = this;
                dw.Show();
            }
        }

        /// <summary>
        /// opens an add drone window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDrones_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow dw = new DroneWindow(ibl);
            dw.Owner = this;
            dw.Show();
            exit = true;
        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        /// <summary>
        /// closes the window only if allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }
        #endregion
    }
}