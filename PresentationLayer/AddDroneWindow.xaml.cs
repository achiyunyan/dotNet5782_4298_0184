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
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        IBL.BO.Drone drone;
        IBL.IBL bl;
        bool[] well = {false,false,false,false };
        //TODO later
        public AddDroneWindow(IBL.IBL myBl)
        { 
            bl = myBl;
            InitializeComponent();
            this.comboInitialStation.ItemsSource = myBl.GetStationsList();
            this.comboMaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategory));
        }

        private void btnSaveCanges_Click(object sender, RoutedEventArgs e)
        {
            if (well.All(pl => pl==true))
            {
                drone = new IBL.BO.Drone();
                drone.Id = int.Parse(Id.Text);
                ListStation x = (ListStation)comboInitialStation.SelectedItem;
                drone.Model = Model.Text;
                drone.WeightCategory = (WeightCategory)comboMaxWeight.SelectedItem;
                try
                {
                    bl.AddDrone(drone, x.Id);
                }
                catch(BL.BlException exem)
                {

                    idExeption.Text = exem.Message;
                }
                MessageBox.Show("successfuly added!");
                btnBackToList_Click(sender,e);
            }

        }

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            new DronesListWindow(bl).Show();
            this.Close();
        }


        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            int id;
            bool success = int.TryParse(Id.Text, out id); 
            if (!success || id < 10000)
            {

                Id.Background = Brushes.Red;
                well[0] = false;
            }
            else
            {
                if (bl.GetDronesList(dr => dr.Id == id).Count() > 0)//==1
                {
                    idExeption.Text = "Already exists!";
                    idExeption.Background = Brushes.Red;
                }
                else
                {
                    idExeption.Text = "";
                    idExeption.Background = Brushes.Aquamarine;
                    Id.Background = Brushes.Aqua;
                    well[0] = true;
                }

            }

        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Model.Text == "")
            {
                Model.Background = Brushes.Red;
                well[1] = false;
            }
            else
            {
                Model.Background = Brushes.AliceBlue;
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
            var x = sender.GetType();
            if (sender.GetType().Name == "AddDroneWindow")
                e.Cancel = true;
        }
    }
}
