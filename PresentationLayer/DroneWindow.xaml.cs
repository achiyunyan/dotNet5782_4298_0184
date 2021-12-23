﻿using BO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>

    /// <summary>
    /// Drone actions functions
    /// </summary>
    public partial class DroneWindow : Window
    {
        BlApi.IBL bl;
        BO.ListDrone ListDrone;
        BO.Drone drone;
        bool[] well = { false, false, false, false };
        bool exit = false;
        public DroneWindow(BO.ListDrone myDrone, BlApi.IBL myBl,DronesListWindow dronesListWindow)
        {
            dlw = dronesListWindow;
            bl = myBl;
            ListDrone = myDrone;
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            Title = "DroneActionsWindow";
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            BO.Drone blDrone = bl.GetDrone(ListDrone.Id);
            DroneId.Text = blDrone.Id.ToString();
            Model.Text = blDrone.Model;
            MaxWeight.Text = blDrone.WeightCategory.ToString();
            Battery.Text = Math.Round(blDrone.Battery).ToString() + '%';
            State.Text = blDrone.State.ToString();
            Location.Text = blDrone.Location.ToString();
            if (blDrone.Parcel != default)
            {
                ParcelTag.Text = "Parcel:";
                Parcel.Text = blDrone.Parcel.ToString();
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

            if (blDrone.State != BO.DroneState.Delivery || blDrone.Parcel.State)
                CollectParcel.Visibility = Visibility.Collapsed;
            else
                CollectParcel.Visibility = Visibility.Visible;

            if (blDrone.State != BO.DroneState.Delivery || !blDrone.Parcel.State)
                DeliverParcel.Visibility = Visibility.Collapsed;
            else
                DeliverParcel.Visibility = Visibility.Visible;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update.Visibility = Visibility.Visible;
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(ListDrone.Id, Model.Text);
            MessageBox.Show("Model updated successfully!");
            dlw.Refresh();
            UpdateWindow();
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
            dlw.Refresh();
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
            dlw.Refresh();
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
            dlw.Refresh();
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
            dlw.Refresh();
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
            dlw.Refresh();
            UpdateWindow();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add drone functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        DronesListWindow dlw;
        public DroneWindow(BlApi.IBL myBl, DronesListWindow dronesListWindow)
        {
            dlw = dronesListWindow;
            bl = myBl;
            InitializeComponent();
            DroneActions.Visibility = Visibility.Hidden;
            Title = "AddDroneWindow";
            this.comboInitialStation.ItemsSource = myBl.GetStationsList();
            this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
        }

        private void btnSaveCanges_Click(object sender, RoutedEventArgs e)
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
            dlw.Refresh();
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
                if (bl.GetDronesList(dr => dr.Id == id).Count() > 0)//==1
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
    }

}