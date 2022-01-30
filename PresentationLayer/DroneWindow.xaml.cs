using BO;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel;

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
            return (bool)value ? "Collected" : "Associated";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LocationToStringConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Location)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DroneStateAvailableToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DroneState)value == DroneState.Available ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DroneStateMaintenanceToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DroneState)value == DroneState.Maintenance ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParcelStateAssociatedToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((ParcelInTransit)value).State ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParcelStateCollectedToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((ParcelInTransit)value).State ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class DroneWindow : Window
    {
        BlApi.IBL bl;
        PO.Drone poDrone;
        private bool[] well = { false, false, false, false };
        private bool exit = false;
        private bool first = true;
        BackgroundWorker worker;
        ParcelWindow parcelWindow;

        /// <summary>
        /// Drone actions functions
        /// </summary>
        #region Drone Actions

        /// <summary>
        /// constractor for drone actions window
        /// </summary>
        /// <param name="myDrone"></param>
        /// <param name="myBl"></param>
        public DroneWindow(BO.ListDrone myDrone, BlApi.IBL myBl)
        {
            bl = myBl;
            poDrone = new PO.Drone(bl.GetDrone(myDrone.Id));
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            Title = "DroneActionsWindow";
            DroneActions.DataContext = poDrone;
        }

        /// <summary>
        /// updates the window 
        /// </summary>
        public void Refresh()
        {
            poDrone = new PO.Drone(bl.GetDrone(poDrone.Id));
            DroneActions.DataContext = poDrone;
            if (parcelWindow != null)
                parcelWindow.Refresh(this);
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            if (Owner is ParcelWindow)
                ((ParcelWindow)this.Owner).Refresh();
        }

        /// <summary>
        /// Show the update button for possible model change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!first)
                Update.Visibility = Visibility.Visible;
            else
                first = false;
        }

        /// <summary>
        /// updates the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(poDrone.Id, Model.Text);
            MessageBox.Show("Model updated successfully!");
            Update.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// sends the drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendToCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to charge successfully!";
            try
            {
                bl.SendDroneToCharge(poDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);

            Refresh();
        }

        /// <summary>
        /// Realse the drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FreeFromCharge_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone freed from charge successfully!";
            try
            {
                bl.DroneRelease(poDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            Refresh();
        }

        /// <summary>
        /// Sends the drone to delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendToDelivery_Click(object sender, RoutedEventArgs e)
        {
            string str = "Drone sent to delivery successfully!";
            try
            {
                bl.LinkParcelToDroneBL(poDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            Refresh();
        }

        /// <summary>
        /// Collect the parcel from the sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel collected successfully!";
            try
            {
                bl.PickParcel(poDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            Refresh();
        }

        /// <summary>
        /// Deliver the parcel to the receiver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliverParcel_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel Delivered successfully!";
            try
            {
                bl.DeliverParcel(poDrone.Id);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
            }
            MessageBox.Show(str);
            Refresh();
        }

        /// <summary>
        /// Opens the parcel window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openParcelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Owner is ParcelWindow)
            {
                Owner.Show();
                return;
            }
            parcelWindow = new ParcelWindow(bl.GetParcelsList().First(pr => pr.Id == poDrone.Parcel.Id), bl)
            {
                Owner = this
            };
            parcelWindow.Show();
        }

        /// <summary>
        /// Turn on simulation mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            Automatic.Visibility = Visibility.Collapsed;
            Manual.Visibility = Visibility.Visible;
            ActionsButtons.Visibility = Visibility.Collapsed;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            worker.DoWork += (sender, args) => bl.StartSimulator(poDrone.Id, updateWindow, checkStop);
            worker.RunWorkerCompleted += (sender, args) => { worker = null; };
            worker.ProgressChanged += (sender, args) => Refresh();
            worker.RunWorkerAsync(poDrone.Id);
        }

        /// <summary>
        /// update window by calling the progressChanged event
        /// </summary>
        private void updateWindow() { worker.ReportProgress(0); }

        /// <summary>
        /// checks if worker was assked to stop
        /// </summary>
        /// <returns></returns>
        private bool checkStop() { return !worker.CancellationPending; }

        /// <summary>
        /// changes the drone mode to manual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            Refresh();
            Automatic.Visibility = Visibility.Visible;
            ActionsButtons.Visibility = Visibility.Visible;
            Manual.Visibility = Visibility.Collapsed;
        }
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add drone functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Add Drone

        /// <summary>
        /// constractor for add drone window
        /// </summary>
        /// <param name="myBl"></param>
        public DroneWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            DroneActions.Visibility = Visibility.Hidden;
            Title = "AddDroneWindow";
            this.comboInitialStation.ItemsSource = myBl.GetStationsList();
            this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
        }

        /// <summary>
        /// adds the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (well.All(pl => pl == true))
            {
                BO.Drone drone = new BO.Drone();
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

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            if (worker != null)
                Manual_Click(sender, e);
            //TO DO - update drone list
            if (Owner is DronesListWindow)
                ((DronesListWindow)this.Owner).Refresh();
            if (Owner is StationWindow)
                ((StationWindow)this.Owner).Refresh();
            exit = true;
            this.Close();
        }

        /// <summary>
        /// gets the id and checks if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    well[0] = false;
                }
                else
                {
                    idExeption.Text = "";
                    Id.Background = null;
                    well[0] = true;
                }
            }

        }

        /// <summary>
        /// gets the model and checks if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ModelAdd.Text == "")
            {
                ModelAdd.Background = Brushes.Tomato;
                well[1] = false;
            }
            else
            {
                ModelAdd.Background = null;
                well[1] = true;
            }
        }

        /// <summary>
        /// valid the max weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboMaxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[2] = true;
        }

        /// <summary>
        /// valid the initial station  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboInitialStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[3] = true;
        }

        /// <summary>
        /// closes the window if allowed
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
