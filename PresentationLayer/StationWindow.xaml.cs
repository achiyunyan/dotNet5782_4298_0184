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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>

    public partial class StationWindow : Window
    {
        BlApi.IBL bl;
        Station station;
        bool exist = false;

        #region Show Station 
        bool firstChange1 = true;
        bool firstChange2 = true;

        /// <summary>
        /// Show station constructor
        /// </summary>
        /// <param name="bL"></param>
        /// <param name="lStation"></param>
        public StationWindow(BlApi.IBL bL, BO.ListStation lStation)
        {
            exist = true;
            bl = bL;
            station = new Station(bl.GetStation(lStation.Id));
            InitializeComponent();
            Refresh();
            AddStation.Visibility = Visibility.Hidden;
            StationUpdate.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Open the droneWindow for the charged drone 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneInCharge droneInCharge = (BO.DroneInCharge)lstvDrones.SelectedItem;
            BO.ListDrone ListDroneInCharge = bl.GetDronesList().First(dr => dr.Id == droneInCharge.Id);
            DroneWindow dw = new DroneWindow(ListDroneInCharge, bl);
            dw.Owner = this;
            dw.Show();

        }

        /// <summary>
        /// Refresh the window so changes will be seen
        /// </summary>
        public void Refresh()
        {
            if (exist)
            {
                station = new Station(bl.GetStation(station.Id));
                StationActions.DataContext = station;
            }
            if (Owner is StationsListWindow)
                ((StationsListWindow)Owner).Refresh();
        }


        /// <summary>
        /// Paint red the background of the field if not valid
        /// If valid, let the user update the station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstChange1)
            {
                firstChange1 = false;
            }
            else
            {
                if (StationName.Text == "")
                {
                    NameExeption.Text = " Name not valid!";
                    StationName.Background = Brushes.Tomato;
                    well[1] = false;
                    StationUpdate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NameExeption.Text = "";
                    StationName.Background = Brushes.MintCream;
                    well[1] = true;
                    StationUpdate.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Paint red the background of the field if not valid
        /// If valid, let the user update the station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationSlots_TextChanged(object sender, RoutedEventArgs e)
        {
            if (firstChange2)
            {
                firstChange2 = false;
            }
            else
            {
                int slots;
                bool success = int.TryParse(StationSlots.Text, out slots);
                if (!success || slots < 0)
                {
                    SlotsExeption.Text = " Free slots not valid!";
                    StationSlots.Background = Brushes.Tomato;
                    well[0] = false;
                    StationUpdate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    StationUpdate.Visibility = Visibility.Visible;
                    SlotsExeption.Text = "";
                    StationSlots.Background = Brushes.MintCream;
                    well[0] = true;
                }
            }
        }

        /// <summary>
        /// Update the station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (well[0] || well[1])
            {
                string str = "Updated succesfully";
                int freeChargingSlots = station.FreeChargeSlots;
                if (uint.TryParse(StationSlots.Text, out _))
                    freeChargingSlots = int.Parse(StationSlots.Text) + ((station.DronesList == null) ? 0 : station.DronesList.Count);
                try
                {
                    bl.UpdateStation(station.Id, StationName.Text, freeChargingSlots);
                }
                catch (BL.BlException exem)
                {
                    str = exem.Message;
                }
                MessageBox.Show(str);
                Refresh();
            }

        }

        /// <summary>
        /// Button 'exit' was clicked
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            this.Close();
        }

        /// <summary>
        /// Let only alowed exit to happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add station functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region addStation functions

        bool[] well = { false, false, false, false, false };
        private bool exit;

        /// <summary>
        /// constructor for add station operations window
        /// </summary>
        /// <param name="myBl"></param>
        public StationWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();;
            StationActions.Visibility = Visibility.Hidden;
            Title = "AddStationWindow";

        }

        /// <summary>
        /// Add station button was clicked
        /// Will add the requested station to the databse if the parameters are legal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStationBtn_Click(object sender, RoutedEventArgs e)
        {
            string str = "Station succesfully added";
            bool error = false;
            if (well.All(property => property == true))
            {
                try
                {
                    BO.Location loc = new BO.Location();
                    loc.Latitude = double.Parse(latitude.Text);
                    loc.Longitude = double.Parse(longitude.Text);
                    bl.AddStation(new BO.Station
                    {
                        Id = int.Parse(Id.Text),
                        Name = Name.Text,
                        FreeChargeSlots = int.Parse(FreeSlots.Text),
                        Location = loc
                    });
                }
                catch (BL.BlException exem)
                {
                    str = exem.Message;
                    error = true;
                }
                MessageBox.Show(str);
                if (!error)
                {
                    if (Owner is StationsListWindow)
                        ((StationsListWindow)Owner).Refresh();
                    btnBackToList_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Inform the user whether if the assigned id is not ok 
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
                if (bl.GetStationsList().Any(st => st.Id == id))//==1
                {
                    idExeption.Text = "Id already exists!";
                }
                else
                {
                    idExeption.Text = "";
                    Id.Background = Brushes.White;
                    well[0] = true;
                }
            }
        }

        /// <summary>
        /// Paint red the background of the field if not valid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name.Text == "")
            {
                Name.Background = Brushes.Tomato;
                well[1] = false;
            }
            else
            {
                Name.Background = Brushes.White;
                well[1] = true;
            }
        }

        /// <summary>
        /// Paint red the background of the field if not valid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FreeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            uint help;
            if (uint.TryParse(FreeSlots.Text, out help))
            {
                FreeSlots.Background = Brushes.White;
                well[2] = true;
            }
            else
            {
                FreeSlots.Background = Brushes.Tomato;
                well[2] = false;
            }
        }

        /// <summary>
        /// Paint red the background of the field if not valid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void longitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double longitudeInt;
            if (double.TryParse(longitude.Text, out longitudeInt))
            {
                //if (longitudeInt > 35.22496332365079 || longitudeInt < 35.16242159781234)
                if (longitudeInt > 180 || longitudeInt < -180)
                {
                    longitude.Background = Brushes.Tomato;
                    well[3] = false;
                }
                else
                {
                    longitude.Background = Brushes.White;
                    well[3] = true;
                }
            }
            else
            {
                longitude.Background = Brushes.Tomato;
                well[3] = false;
            }
        }

        /// <summary>
        /// Paint red the background of the field if not valid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void latitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitudeInt;
            if (double.TryParse(latitude.Text, out latitudeInt))
            {
                //if (latitudeInt > 31.809648051878856 || latitudeInt < 31.742227429597634)
                if (latitudeInt > 90 || latitudeInt < -90)
                {
                    latitude.Background = Brushes.Tomato;
                    well[4] = false;
                }
                else
                {
                    latitude.Background = Brushes.AliceBlue;
                    well[4] = true;
                }
            }
            else
            {
                latitude.Background = Brushes.Tomato;
                well[4] = false;
            }

        }
        #endregion
    }
}
