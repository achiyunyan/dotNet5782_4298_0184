using BO;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>

    public class BoolStateToStringConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool State = (bool)value;
            return State ? "Collected" : "Associated";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class DroneWindow : Window
    {
        BlApi.IBL bl;
        BO.ListDrone ListDrone;
        BO.Drone drone;
        bool[] well = { false, false, false, false };
        bool exit = false;
        bool first = true;

        /// <summary>
        /// Drone actions functions
        /// </summary>

        public DroneWindow(BO.ListDrone myDrone, BlApi.IBL myBl)
        {
            bl = myBl;
            ListDrone = myDrone;
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            Title = "DroneActionsWindow";            
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            drone = bl.GetDrone(ListDrone.Id);
            DroneActions.DataContext = drone;

            if (drone.Parcel != default)
            {
                ParcelTag.Visibility = Visibility.Visible; 
                Parcel.Visibility = Visibility.Visible;
                OpenParcel.Visibility = Visibility.Visible;               
            }
            else
            {
                ParcelTag.Visibility = Visibility.Hidden;
                Parcel.Visibility = Visibility.Hidden;
                OpenParcel.Visibility = Visibility.Collapsed;
            }

            if (ListDrone.State != BO.DroneState.Available)
                SendToCharge.Visibility = Visibility.Collapsed;
            else
                SendToCharge.Visibility = Visibility.Visible;

            if (ListDrone.State != BO.DroneState.Maintenance)
                FreeFromCharge.Visibility = Visibility.Collapsed;
            else
                FreeFromCharge.Visibility = Visibility.Visible;

            if (ListDrone.State != BO.DroneState.Available)
                SendToDelivery.Visibility = Visibility.Collapsed;
            else
                SendToDelivery.Visibility = Visibility.Visible;

            if (drone.State != BO.DroneState.Delivery || drone.Parcel.State)
                CollectParcel.Visibility = Visibility.Collapsed;
            else
                CollectParcel.Visibility = Visibility.Visible;

            if (drone.State != BO.DroneState.Delivery || !drone.Parcel.State)
                DeliverParcel.Visibility = Visibility.Collapsed;
            else
                DeliverParcel.Visibility = Visibility.Visible;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!first)
                Update.Visibility = Visibility.Visible;
            else
                first = false;
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(ListDrone.Id, Model.Text);
            MessageBox.Show("Model updated successfully!");
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            Update.Visibility = Visibility.Collapsed;
        }

        private void SendToCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to charge successfully!";
            try
            {
                bl.SendDroneToCharge(ListDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            if(Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            UpdateWindow();
        }

        private void FreeFromCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone freed from charge successfully!";
            try
            {
                bl.DroneRelease(ListDrone.Id, 3);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            UpdateWindow();
        }

        private void SendToDelivery_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to delivery successfully!";
            try
            {
                bl.LinkParcelToDroneBL(ListDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            UpdateWindow();
        }

        private void CollectParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel collected successfully!";
            try
            {
                bl.PickParcel(ListDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            UpdateWindow();
        }

        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel Delivered successfully!";
            try
            {
                bl.DeliverParcel(ListDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            UpdateWindow();
        }        

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add drone functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DroneWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            DroneActions.Visibility = Visibility.Hidden;
            Title = "AddDroneWindow";
            this.comboInitialStation.ItemsSource = myBl.GetStationsList();
            this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
        }

        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (well.All(pl => pl == true))
            {
                drone = new BO.Drone();
                drone.Id = int.Parse(Id.Text);
                ListStation x = (ListStation)comboInitialStation.SelectedItem;
                drone.Model = ModelAdd.Text;
                drone.WeightCategory = (WeightCategory)comboMaxWeight.SelectedItem;
                string str = "Drone successfuly added!";
                bool error = false;
                try
                {
                    bl.AddDrone(drone, x.Id);
                }
                catch (BL.BlException exem)
                {
                    str = exem.Message;
                    error = true;
                }
                MessageBox.Show(str);
                if (!error)
                {
                    btnBackToList_Click(sender, e);
                }
            }

        }

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            //TO DO - update drone list
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            exit = true;
            this.Close();
        }


        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            int id;
            bool success = int.TryParse(Id.Text, out id);
            if (!success || id < 10000)
            {
                idExeption.Text = "Id not valid!";
                Id.Background = Brushes.Tomato;
                well[0] = false;
            }
            else
            {
                if (bl.GetDronesList(dr => dr.Id == id).Any())//==1
                {
                    idExeption.Text = "Id already exists!";
                }
                else
                {
                    idExeption.Text = "";
                    Id.Background = Brushes.Aqua;
                    well[0] = true;
                }
            }

        }

        private void ModelAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ModelAdd.Text == "")
            {
                ModelAdd.Background = Brushes.Tomato;
                well[1] = false;
            }
            else
            {
                ModelAdd.Background = Brushes.AliceBlue;
                well[1] = true;
            }
        }

        private void comboMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[2] = true;
        }

        private void comboInitialStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[3] = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        private void OpenParcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }    
}
