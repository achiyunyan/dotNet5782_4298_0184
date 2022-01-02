using BO;
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

    public class NullToCollapsedConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToVisibleConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ParcelWindow : Window
    {
        BlApi.IBL bl;
        Parcel parcel; 
        bool[] well = { false, false, false, false };
        bool exit = false;
        bool first = true;
        ParcelsListWindow plw;
        /// <summary>
        /// Drone actions functions
        /// </summary>
        /// 
        public ParcelWindow(BO.ListParcel myParcel, BlApi.IBL myBl, ParcelsListWindow parcelsListWindow)
        {
            plw = parcelsListWindow;
            bl = myBl;
            parcel = bl.GetParcel(myParcel.Id);
            InitializeComponent();
            AddParcel.Visibility = Visibility.Hidden;
            Title = "DroneActionsWindow";
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            ParcelActions.DataContext = parcel;
            if (parcel.Drone == null || parcel.Delivered != null)
            {
                openDroneBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                openDroneBtn.Visibility = Visibility.Visible;
            }           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            plw.Refresh();
            exit = true;
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string str = "Parcel deleted successfully!";
            bool success = true;
            try
            {
                bl.DeleteParcel(parcel);
            }
            catch (BL.BlException exem)
            {
                str = exem.Message;
                success = false;
            }
            MessageBox.Show(str);
            if (success)
                btnBackToList_Click(sender, e);            
        }

        private void openDroneBtn_Click(object sender, RoutedEventArgs e)
        { 

        }

        private void openSenderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openReceiverBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
