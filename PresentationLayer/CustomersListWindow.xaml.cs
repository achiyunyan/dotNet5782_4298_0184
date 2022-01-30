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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersListWindow.xaml
    /// </summary>    

    public partial class CustomersListWindow : Window
    {
        BlApi.IBL ibl;
        bool exit = false;

        /// <summary>
        /// constarctor
        /// </summary>
        /// <param name="myBl"></param>
        public CustomersListWindow(BlApi.IBL myBl)
        {
            ibl = myBl;
            InitializeComponent();
            lstvCustomers.ItemsSource = ibl.GetCustomersList();
        }

        #region Actions
        /// <summary>
        /// opens a customer actions window for the selected customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openActionsWindow(object sender, MouseButtonEventArgs e)
        {
            if (lstvCustomers.Items.Count > 0)
            {
                CustomerWindow pw = new CustomerWindow((ListCustomer)lstvCustomers.SelectedItem, ibl);
                pw.Owner = this;
                pw.Show();
            }
        }

        /// <summary>
        /// closes the window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        /// <summary>
        /// close the window if allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        /// <summary>
        /// opens an add customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow(ibl);
            cw.Owner = this;
            cw.Show();
            exit = true;
        }

        /// <summary>
        /// Update the customer list
        /// </summary>
        public void Refresh()
        {
            lstvCustomers.ItemsSource = ibl.GetCustomersList();
        }
        #endregion

    }
}
