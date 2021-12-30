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
    /// Interaction logic for ParcelsListWindow.xaml
    /// </summary>
    public partial class ParcelsListWindow : Window
    {
        BlApi.IBL ibl;
        bool exit = false;

        public ParcelsListWindow(BlApi.IBL myBl)
        {
            ibl = myBl;
            InitializeComponent();
            parcelsList.ItemsSource = ibl.GetParcelsList();

            comboPriority.Items.Add("");
            foreach (var x in Enum.GetValues(typeof(Priority)))
            {
                comboPriority.Items.Add(x);
            }
            
            comboState.Items.Add("");
            foreach (var x in Enum.GetValues(typeof(ParcelState)))
            {
                comboState.Items.Add(x);
            }

            comboWeight.Items.Add("");
            foreach (var x in Enum.GetValues(typeof(WeightCategory)))
            {
                comboWeight.Items.Add(x);
            }

            comboReceiver.ItemsSource = ((IEnumerable<ListParcel>)parcelsList.ItemsSource)
                                        .GroupBy(pr => pr.ReceiverName)
                                        .Select(g => g.First().ReceiverName)
                                        .OrderBy(name => name)
                                        .Prepend("");

            comboSender.ItemsSource = ((IEnumerable<ListParcel>)parcelsList.ItemsSource)
                                        .GroupBy(pr => pr.SenderName)
                                        .Select(g => g.First().SenderName)
                                        .OrderBy(name => name)
                                        .Prepend("");           
        }

        private void btnAddParcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            exit = true;
            Close();
        }

        private void btnGroupBySenderName_Click(object sender, RoutedEventArgs e)
        {
            parcelsList.ItemsSource = from parcel in (IEnumerable<ListParcel>)parcelsList.ItemsSource
                                      orderby parcel.SenderName
                                      select parcel;
        }

        private void btnGroupByReceiverName_Click(object sender, RoutedEventArgs e)
        {
            parcelsList.ItemsSource = from parcel in (IEnumerable<ListParcel>)parcelsList.ItemsSource
                                      orderby parcel.ReceiverName
                                      select parcel;
        }

        private void comboState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void comboSender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void comboReceiver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void comboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void comboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            IEnumerable<ListParcel> parcels = ibl.GetParcelsList();

            if (!(comboSender.SelectedItem == null || comboSender.SelectedItem == ""))
                parcels = parcels.Where(par => par.SenderName == comboSender.SelectedItem);

            if (!(comboReceiver.SelectedItem == null || comboReceiver.SelectedItem == ""))
                parcels = parcels.Where(par => par.ReceiverName == comboReceiver.SelectedItem);

            if (!(comboPriority.SelectedItem == null || comboPriority.SelectedItem == ""))
                parcels = parcels.Where(par => par.Priority == (Priority)comboPriority.SelectedItem);

            if (!(comboState.SelectedItem == null || comboState.SelectedItem == ""))
                parcels = parcels.Where(par => par.State == (ParcelState)comboState.SelectedItem);

            if (!(comboWeight.SelectedItem == null || comboWeight.SelectedItem == ""))
                parcels = parcels.Where(par => par.WeightCategory == (WeightCategory)comboWeight.SelectedItem);

            parcelsList.ItemsSource = parcels;
        }
    }
}
