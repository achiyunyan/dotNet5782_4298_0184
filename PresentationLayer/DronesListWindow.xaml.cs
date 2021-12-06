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
        public DronesListWindow(IBL.IBL myBl)
        {
            ibl = myBl;

            InitializeComponent();
            this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
            this.comboStatus.ItemsSource = Enum.GetValues(typeof(DroneState));
            this.lstvDrones.ItemsSource = ibl.GetDronesList();
        }

        private void comboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneState state = (DroneState) comboStatus.SelectedItem;
            this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.State == state);
        }
        private void comboMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeightCategory maxWeight = (WeightCategory)comboMaxWeight.SelectedItem;
            this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.WeightCategory == maxWeight);
        }
        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(ibl).Show();
        }
        private void openActionsWindow(object sender, MouseButtonEventArgs e)
        {
            new DroneActionsWindow((ListDrone)lstvDrones.SelectedItem, ibl).Show();
        }
    } 
}
