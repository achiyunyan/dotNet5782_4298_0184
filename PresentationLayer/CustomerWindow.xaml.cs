using PO;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        #region config
        BlApi.IBL bl;
        Customer customer;
        bool[] well = { false, false, false, false, false };
        bool exit = false;
        private bool first1 = true;
        private bool first2 = true;
        private bool phoneIsOk = true;
        private bool openParcels;
        #endregion

        #region show customer
        /// <summary>
        /// ctor for show customer window
        /// </summary>
        /// <param name="myCustomer"></param>
        /// <param name="myBl"></param>
        /// <param name="openParcels"></param>
        public CustomerWindow(BO.ListCustomer myCustomer, BlApi.IBL myBl, bool openParcels = true)
        {
            bl = myBl;
            customer = new Customer(bl.GetCustomer(myCustomer.Id));
            this.openParcels = openParcels;
            InitializeComponent();
            AddCustomer.Visibility = Visibility.Hidden;
            Title = "CustomerActionsWindow";
            CustomerActions.DataContext = customer;
        }


        /// <summary>
        /// Let only allowed exit to happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        /// <summary>
        /// Refresh the customer presented in the window and int owner
        /// </summary>
        public void Refresh()
        {
            if (CustomerActions.Visibility == Visibility.Visible)
            {
                customer = new Customer(bl.GetCustomer(customer.Id));
                CustomerActions.DataContext = customer;
            }
            if (Owner is CustomersListWindow)
                ((CustomersListWindow)this.Owner).Refresh();
            if (Owner is ParcelWindow)
                ((ParcelWindow)this.Owner).Refresh();
        }

        /// <summary>
        /// Exit the window
        /// Open other window if nececcary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            exit = true;
            if (!openParcels)
                new EntrenceWindow().Show();
            this.Close();
        }

        /// <summary>
        ///Phone was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!first1 && ulong.TryParse(Phone.Text, out _) && (Phone.Text.Length == 10 || Phone.Text.Length == 9))
            {
                phoneIsOk = true;
                Phone.Background = Brushes.MintCream;
                Phone.Foreground = Brushes.Teal;
                phoneExp.Text = " ";
                Update.Visibility = Visibility.Visible;
            }
            else
            {
                if (!first1)
                {
                    phoneIsOk = false;
                    Update.Visibility = Visibility.Collapsed;
                    Phone.Background = Brushes.Tomato;
                    Phone.Foreground = Brushes.White;
                    phoneExp.Text = " Phone not valid!";
                }
                else
                    first1 = false;
            }
        }

        /// <summary>
        /// Name was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!first2 && phoneIsOk)
                Update.Visibility = Visibility.Visible;
            else
                first2 = false;
        }

        /// <summary>
        /// Update the customer details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateCustomer(customer.Id, Name.Text, Phone.Text);
            MessageBox.Show("Customer updated successfully!");
            Update.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// One of the parcels the customer sent was clicked. Open its window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (openParcels)
            {
                ParcelWindow pw = new ParcelWindow(bl.GetParcelsList().First(pr => pr.Id == ((BO.ParcelInCustomer)OutParcelsList.SelectedItem).Id), bl);
                pw.Owner = this;
                pw.Show();
            }
        }

        /// <summary>
        ///  One of the parcels sent to the customer was clicked. Open its window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (openParcels)
            {
                ParcelWindow pw = new ParcelWindow(bl.GetParcelsList().First(pr => pr.Id == ((BO.ParcelInCustomer)InParcelsList.SelectedItem).Id), bl);
                pw.Owner = this;
                pw.Show();
            }
        }

        /// <summary>
        /// Open the window to add parcel wich will be sent from this customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow pw = new ParcelWindow(bl, customer.Id);
            pw.Owner = this;
            pw.Show();
        }
#endregion 
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add customer functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Add customer


        public CustomerWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            CustomerActions.Visibility = Visibility.Hidden;
            Title = "AddCustomerWindow";            
        }

        /// <summary>
        /// Id was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            uint id;
            if (uint.TryParse(IdGet.Text, out id))
            {
                IdExeption.Text = "";
                IdGet.Background = Brushes.White;
                well[0] = true;
            }
            else
            {
                IdExeption.Text = "Id not valid!";
                IdGet.Background = Brushes.Tomato;
                well[0] = false;
            }
        }

        /// <summary>
        /// Name was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameGet.Text != "")
            {
                NameExeption.Text = "";
                NameGet.Background = Brushes.White;
                well[1] = true;
            }
            else
            {
                NameExeption.Text = "Name not valid!";
                NameGet.Background = Brushes.Tomato;
                well[1] = false;
            }
        }

        /// <summary>
        /// Phone was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            ulong phone;
            if (ulong.TryParse(PhoneGet.Text, out phone) && (PhoneGet.Text.Length == 10 || PhoneGet.Text.Length == 9))
            {
                PhoneExeption.Text = "";
                PhoneGet.Background = Brushes.White;
                well[2] = true;
            }
            else
            {
                PhoneExeption.Text = "Phone number not valid!";
                PhoneGet.Background = Brushes.Tomato;
                well[2] = false;
            }
        }

        /// <summary>
        /// Latitude was changed
        /// Update the relevant fields and textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationLatGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            double l;
            if (double.TryParse(LocationLatGet.Text, out l) && l >= -90 && l <= 90) 
            {
                LocationLatExeption.Text = "";
                LocationLatGet.Background = Brushes.White;
                well[3] = true;
            }
            else
            {
                LocationLatExeption.Text = "Latitude  not valid!";
                LocationLatGet.Background = Brushes.Tomato;
                well[3] = false;
            }
        }

        /// <summary>
        /// Longitude was changed
        /// Update the relevant fields and textboxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationLongGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            double l;
            if (double.TryParse(LocationLongGet.Text, out l) && l >= -180 && l <= 180)
            {
                LocationLongExeption.Text = "";
                LocationLongGet.Background = Brushes.White;
                well[4] = true;
            }
            else
            {
                LocationLongExeption.Text = "Longitude  not valid!";
                LocationLongGet.Background = Brushes.Tomato;
                well[4] = false;
            }
        }
        
        /// <summary>
        /// Add the customer if everything is legal
        /// Update the user about the result of the action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(well.All(pl => pl == true))
            {
                string str = "Customer successfuly added!";
                bool error = false;
                try
                {
                    bl.AddCustomer(new BO.Customer
                    { 
                        Id = int.Parse(IdGet.Text),
                        Name = NameGet.Text,
                        Phone =PhoneGet.Text,
                        Location = new BO.Location { 
                            Latitude = double.Parse(LocationLatGet.Text), 
                            Longitude = double.Parse(LocationLongGet.Text)}
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
        #endregion

    }
}
