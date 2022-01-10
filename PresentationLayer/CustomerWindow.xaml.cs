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
        BlApi.IBL bl;
        Customer customer;
        bool[] well = { false, false, false, false };
        bool exit = false;
        private bool first1 = true;
        private bool first2 = true;
        private bool phoneIsOk = true;
        /// <summary>
        /// Customer actions functions
        /// </summary>
        /// 
        public CustomerWindow(BO.ListCustomer myCustomer, BlApi.IBL myBl)
        {
            bl = myBl;
            customer = new Customer(bl.GetCustomer(myCustomer.Id));
            InitializeComponent();
            AddCustomer.Visibility = Visibility.Hidden;
            Title = "CustomerActionsWindow";
            CustomerActions.DataContext = customer;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

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

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            exit = true;
            this.Close();
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ulong phone;
            if (!first1 && ulong.TryParse(Phone.Text, out phone) && (Phone.Text.Length == 11 || Phone.Text.Length == 10))
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

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!first2 && phoneIsOk)
                Update.Visibility = Visibility.Visible;
            else
                first2 = false;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateCustomer(customer.Id, Name.Text, Phone.Text);
            MessageBox.Show("Customer updated successfully!");
            Update.Visibility = Visibility.Collapsed;
        }

        private void outParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelWindow pw = new ParcelWindow(bl.GetParcelsList().First(pr => pr.Id == ((BO.ParcelInCustomer)OutParcelsList.SelectedItem).Id), bl);
            pw.Owner = this;
            pw.Show();
        }

        private void InParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelWindow pw = new ParcelWindow(bl.GetParcelsList().First(pr => pr.Id == ((BO.ParcelInCustomer)InParcelsList.SelectedItem).Id), bl);
            pw.Owner = this;
            pw.Show();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add drone functions
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public CustomerWindow(BlApi.IBL myBl)
        {
            bl = myBl;
            InitializeComponent();
            CustomerActions.Visibility = Visibility.Hidden;
            Title = "AddCustomerWindow";            
        }

        
    }
}
