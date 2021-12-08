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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneActionsWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL bl;
        IBL.BO.ListDrone drone;
        public DroneWindow(IBL.BO.ListDrone myDrone, IBL.IBL myBl)
        {
            bl = myBl;
            drone = myDrone;
            InitializeComponent();
            IBL.BO.Drone blDrone = bl.GetDrone(drone.Id);
        }
    }
}
