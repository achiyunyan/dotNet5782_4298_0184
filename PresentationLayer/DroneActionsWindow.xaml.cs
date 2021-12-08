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
            DroneId.Text = blDrone.Id.ToString();
            Model.Text = blDrone.Model;
            MaxWeight.Text = blDrone.WeightCategory.ToString();
            Battery.Text = Math.Round(blDrone.Battery).ToString() + '%';
            State.Text = blDrone.State.ToString();
            Location.Text = blDrone.Location.ToString();
            if(blDrone.Parcel != default)
            {
                ParcelTag.Text = "Parcel:";
                ParcelTag.BorderThickness = new Thickness(1);
                Parcel.Text = blDrone.Parcel.ToString();
                Parcel.BorderThickness = new Thickness(1);
            }
            Update.Visibility = Visibility.Collapsed;
            if (drone.State != IBL.BO.DroneState.Available)
                SendToCharge.Visibility = Visibility.Collapsed;
            if (drone.State != IBL.BO.DroneState.Maintenance)
                FreeFromCharge.Visibility = Visibility.Collapsed;
            if (drone.State != IBL.BO.DroneState.Available)
                SendToDelivery.Visibility = Visibility.Collapsed;
            if (blDrone.State != IBL.BO.DroneState.Delivery || !blDrone.Parcel.State)
                CollectParcel.Visibility = Visibility.Collapsed;
            if (blDrone.State != IBL.BO.DroneState.Delivery || blDrone.Parcel.State)
                DeliverParcel.Visibility = Visibility.Collapsed;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(drone.Id, Model.Text);
            Update.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DronesListWindow(bl).Show();
            Close();
        }

        private void SendToCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to charge successfully!";
            try 
            {
                bl.SendDroneToCharge(drone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
        }

        private void FreeFromCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone freed from charge successfully!";
            try
            {
                bl.DroneRelease(drone.Id,3);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
        }

        private void SendToDelivery_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to delivery successfully!";
            try
            {
                bl.LinkParcelToDroneBL(drone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
        }

        private void CollectParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel collected successfully!";
            try
            {
                bl.PickParcel(drone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
        }

        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel Delivered successfully!";
            try
            {
                bl.DeliverParcel(drone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
        }
    }
}
