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
        public CustomersListWindow(BlApi.IBL myBl)
        {
            ibl = myBl;
            InitializeComponent();
            lstvCustomers.ItemsSource = ibl.GetCustomersList();
        }
        private void openActionsWindow(object sender, MouseButtonEventArgs e)
        {
            CustomerWindow pw = new CustomerWindow((ListCustomer)lstvCustomers.SelectedItem, ibl);
            pw.Owner = this;
            pw.Show();
        }
        private void btnAddDrones_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow pw = new CustomerWindow(ibl);
            pw.Owner = this;
            pw.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            exit = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }
        public void Refresh()
        {
            lstvCustomers.ItemsSource = ibl.GetCustomersList();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow(ibl);
            cw.Owner = this;
            cw.Show();
            exit = true;
        }
    }
}
