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
        bool firstChange1 = true;
        bool firstChange2 = true;
        public StationWindow(BlApi.IBL bL, BO.ListStation lStation)
        {
            bl = bL;
            station = new Station(bl.GetStation(lStation.Id));
            InitializeComponent();
            AddStation.Visibility = Visibility.Hidden;
            lstvDrones.ItemsSource = station.DronesList;
            StackPanelDrone.DataContext = station;
            StationUpdate.Visibility = Visibility.Collapsed;
        }

        private void lstvDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneInCharge droneInCharge = (BO.DroneInCharge)lstvDrones.SelectedItem;
            BO.ListDrone ListDroneInCharge = bl.GetDronesList().First(dr => dr.Id == droneInCharge.Id);
            DroneWindow dw = new DroneWindow(ListDroneInCharge, bl);
            dw.Owner = this;
            dw.Show();

        }

        public void Refresh()
        {
            station = new Station(bl.GetStation(station.Id));
            StackPanelDrone.DataContext = station;
            lstvDrones.ItemsSource = bl.GetStation(station.Id).DronesList;
            if (Owner is StationsListWindow)
                ((StationsListWindow)Owner).Refresh();
        }

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

        private void StationUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (well[0] || well[1])
            {
                string str = "Updated succesfully";
                int chargingSlots = 0;
                int help;
                if (int.TryParse(StationSlots.Text, out help))
                    chargingSlots = int.Parse(StationSlots.Text) + ((station.DronesList == null) ? 0 : station.DronesList.Count);
                try
                {
                    bl.UpdateStation(station.Id, StationName.Text, chargingSlots);
                }
                catch (BL.BlException exem)
                {
                    str = exem.Message;
                }
                MessageBox.Show(str);
                Refresh();
            }

        }

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            this.Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add station functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        bool[] well = { false, false, false, false, false };
        private bool exit;
        private bool error;

        public StationWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            int[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            comboFreeSlots.ItemsSource = x;
            StationActions.Visibility = Visibility.Hidden;
            Title = "AddStationWindow";

        }

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
                        FreeChargeSlots = int.Parse(comboFreeSlots.SelectedItem.ToString()),
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
                    btnBackToList_Click(sender, e);
                }
            }
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
                if (bl.GetStationsList().Any(st => st.Id == id))//==1
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

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name.Text == "")
            {
                Name.Background = Brushes.Tomato;
                well[1] = false;
            }
            else
            {
                Name.Background = Brushes.AliceBlue;
                well[1] = true;
            }
        }

        private void comboFreeSlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[2] = true;
        }

        private void longitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double longitudeInt;
            if (double.TryParse(longitude.Text, out longitudeInt))
            {
                if (longitudeInt > 35.22496332365079 || longitudeInt < 35.16242159781234)
                {
                    longitude.Background = Brushes.Tomato;
                    well[3] = false;
                }
                else
                {
                    longitude.Background = Brushes.AliceBlue;
                    well[3] = true;
                }
            }
            else
            {
                longitude.Background = Brushes.Tomato;
                well[3] = false;
            }
        }

        private void latitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitudeInt;
            if (double.TryParse(latitude.Text, out latitudeInt))
            {
                if (latitudeInt > 31.809648051878856 || latitudeInt < 31.742227429597634)
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

    }
}
