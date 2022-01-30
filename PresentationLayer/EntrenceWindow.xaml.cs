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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntrenceWindow.xaml
    /// </summary>
    public partial class EntrenceWindow : Window
    {
        IBL myBl=BlFactory.GetBl();
        public EntrenceWindow()
        {
            InitializeComponent();  
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            uint id;
            if (uint.TryParse(Id.Text,out id))
            {
                BO.ListCustomer listCustomer = myBl.GetCustomersList().FirstOrDefault(cus => cus.Id == id);
                if (listCustomer == default)
                {
                    IdExeption.Text = "Id not exist!";
                    Id.Background = Brushes.Tomato;
                }
                else
                {
                    CustomerWindow cw = new CustomerWindow(listCustomer, myBl, false);
                    cw.Show();
                    this.Close();
                }
            }

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow(myBl);
            cw.Show();
            Close();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(myBl).Show();
            this.Close();
        }

        private void btnId_TextChanged(object sender, TextChangedEventArgs e)
        {
            uint id;
            if (uint.TryParse(Id.Text, out id))
            {
                IdExeption.Text = "";
                Id.Background = Brushes.MintCream;
            }
            else
            {
                IdExeption.Text = "Id not valid!";
                Id.Background = Brushes.Tomato;
            }
        }
    }
}
