using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    ///     

    public partial class ParcelWindow : Window
    {
        BlApi.IBL bl;
        Parcel parcel;
        bool[] well = { false, false, false, false };
        bool exit = false;
        bool exist = false;
        /// <summary>
        /// Parcel actions functions
        /// </summary>
        /// 
        public ParcelWindow(BO.ListParcel myParcel, BlApi.IBL myBl)
        {
            exist = true;
            bl = myBl;
            parcel = new Parcel(bl.GetParcel(myParcel.Id));
            InitializeComponent();
            AddParcel.Visibility = Visibility.Hidden;
            Title = "ParcelActionsWindow";
            ParcelActions.DataContext = parcel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        public void Refresh(Window w = null)
        {
            if(exist)
                parcel = new Parcel(bl.GetParcel(parcel.Id));
            ParcelActions.DataContext = parcel;
            if (Owner is ParcelsListWindow)
                ((ParcelsListWindow)this.Owner).Refresh();
            if (Owner != w && Owner is DroneWindow)
                ((DroneWindow)this.Owner).Refresh();
            if (Owner is CustomerWindow)
                ((CustomerWindow)this.Owner).Refresh();
        }

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            exit = true;
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel deleted successfully!";
            bool success = true;
            try
            {
                bl.DeleteParcel(parcel.GetBlParcel());
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
                success = false;
            }
            MessageBox.Show(str);
            if (success)
            {
                exist = false;
                btnBackToList_Click(sender, e);
            }
        }

        private void openDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWindow = new DroneWindow(bl.GetDronesList().First(dr => dr.Id == parcel.Drone.Id), bl)
            {
                Owner = this
            };
            droneWindow.Show();
        }

        private void openReceiverBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow(bl.GetCustomersList().First(cs => cs.Id == parcel.Receiver.Id), bl);
            cw.Owner = this;
            cw.Show();
        }

        private void openSenderBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow(bl.GetCustomersList().First(cs => cs.Id == parcel.Sender.Id), bl);
            cw.Owner = this;
            cw.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add Parcel functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ParcelWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            ParcelActions.Visibility = Visibility.Hidden;
            Title = "AddParcelWindow";
            comboPriority.ItemsSource = Enum.GetValues(typeof(BO.Priority));
            comboWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
        }

        private void addParcelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (well.All(pl => pl == true))
            {
                string str = "Parcel successfuly added!";
                bool error = false;
                try
                {
                    bl.AddParcel(int.Parse(SenderId.Text), int.Parse(ReceiverId.Text), (int)(BO.WeightCategory)comboWeight.SelectedItem, (int)(BO.Priority)comboPriority.SelectedItem);
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

        private void SenderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int id;
            bool success = int.TryParse(SenderId.Text, out id);
            if (!success || bl.GetCustomersList().All(cs => cs.Id != id))
            {
                SenderIdExeption.Text = "Id doesn't exists!";
                SenderId.Background = Brushes.Tomato;
                well[0] = false;
            }
            else
            {
                SenderIdExeption.Text = "";
                SenderId.Background = null;
                well[0] = true;
            }
        }
        private void ReceiverId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int id;
            bool success = int.TryParse(ReceiverId.Text, out id);
            if (!success || bl.GetCustomersList().All(cs => cs.Id != id))
            {
                ReceiverIdExeption.Text = "Id doesn't exists!";
                ReceiverId.Background = Brushes.Tomato;
                well[1] = false;
            }
            else
            {
                ReceiverIdExeption.Text = "";
                ReceiverId.Background = null;
                well[1] = true;
            }
        }

        private void comboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[2] = true;
        }

        private void comboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            well[3] = true;
        }
    }
}
