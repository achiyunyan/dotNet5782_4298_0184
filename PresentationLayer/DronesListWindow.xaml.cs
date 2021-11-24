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

namespace PresentationLayer
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

            this.comboStatus.ItemsSource = Enum.GetValues(typeof(DroneState));
            this.comboStatus.Text = "Choose status:";
        }

        private void comboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneState state = (DroneState) comboStatus.SelectedItem;
            this.lstvDrones.ItemsSource = ibl.GetDronesList(drone => drone.State == state);
        }
    }
}
