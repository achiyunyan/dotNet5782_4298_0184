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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplaydronesWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL.IBL ibl;
        bool exit = false;
        public DronesListWindow(IBL.IBL myBl)
        {
            ibl = myBl;

            InitializeComponent();
            //this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
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
        private void openActionsWindow(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow((ListDrone)lstvDrones.SelectedItem, ibl).Show();
        }
        private void btnAddDrones_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(ibl, this).Show();
            exit = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            exit = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }
        public void refresh()
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
    }
}
