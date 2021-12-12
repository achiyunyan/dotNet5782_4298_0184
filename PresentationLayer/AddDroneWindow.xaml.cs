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
        bool exit = false;
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
                ModelAdd.Background = Brushes.Red;
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
            if (exit==false)
                e.Cancel = true;
        }
    }
}
