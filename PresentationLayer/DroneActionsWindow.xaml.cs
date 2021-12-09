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
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            IBL.BO.Drone blDrone = bl.GetDrone(drone.Id);
            DroneId.Text = blDrone.Id.ToString();
            Model.Text = blDrone.Model;
            MaxWeight.Text = blDrone.WeightCategory.ToString();
            Battery.Text = Math.Round(blDrone.Battery).ToString() + '%';
            State.Text = blDrone.State.ToString();
            Location.Text = blDrone.Location.ToString();
            if (blDrone.Parcel != default)
            {
                ParcelTag.Text = "Parcel:";
                ParcelTag.BorderThickness = new Thickness(1);
                ParcelTag.Background = Brushes.White;
                Parcel.Text = blDrone.Parcel.ToString();                
                Parcel.BorderThickness = new Thickness(1);
                Parcel.Background = Brushes.White;                
            }
            else
            {
                ParcelTag.Text = "";
                ParcelTag.BorderThickness = new Thickness(0);
                ParcelTag.Background = Brushes.MintCream;
                Parcel.Text = "";
                Parcel.BorderThickness = new Thickness(0);
                Parcel.Background = Brushes.MintCream;
            }

            Update.Visibility = Visibility.Collapsed;

            if (drone.State != IBL.BO.DroneState.Available)
                SendToCharge.Visibility = Visibility.Collapsed;
            else
                SendToCharge.Visibility = Visibility.Visible;

            if (drone.State != IBL.BO.DroneState.Maintenance)
                FreeFromCharge.Visibility = Visibility.Collapsed;
            else
                FreeFromCharge.Visibility = Visibility.Visible;

            if (drone.State != IBL.BO.DroneState.Available)
                SendToDelivery.Visibility = Visibility.Collapsed;
            else
                SendToDelivery.Visibility = Visibility.Visible;

            if (blDrone.State != IBL.BO.DroneState.Delivery || blDrone.Parcel.State)
                CollectParcel.Visibility = Visibility.Collapsed;
            else
                CollectParcel.Visibility = Visibility.Visible;

            if (blDrone.State != IBL.BO.DroneState.Delivery || !blDrone.Parcel.State)
                DeliverParcel.Visibility = Visibility.Collapsed;
            else
                DeliverParcel.Visibility = Visibility.Visible;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update.Visibility = Visibility.Visible;
        }

private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            new DronesListWindow(bl).Show();
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(drone.Id, Model.Text);
            MessageBox.Show("Model updated successfully!");
            UpdateWindow();
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
            MessageBox.Show(str);
            UpdateWindow();
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
            MessageBox.Show(str);
            UpdateWindow();
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
            MessageBox.Show(str);
            UpdateWindow();
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
            MessageBox.Show(str);
            UpdateWindow();
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
            MessageBox.Show(str);
            UpdateWindow();
        }
    }
}
