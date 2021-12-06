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

            this.comboStatus.ToolTip = "Choose status:";
            this.comboStatus.ItemsSource = Enum.GetValues(typeof(DroneState));
        }

        private void comboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneState state = (DroneState) comboStatus.SelectedItem;
            this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.State == state);
        }
        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(ibl).Show();
        }
    }
}
