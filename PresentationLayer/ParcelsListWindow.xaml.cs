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

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="myBl"></param>
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

        #region View Actions
        /// <summary>
        /// group list by sender name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySenderName_Click(object sender, RoutedEventArgs e)
        {
            parcelsList.ItemsSource = from parcel in (IEnumerable<ListParcel>)parcelsList.ItemsSource
                                      orderby parcel.SenderName
                                      select parcel;
        }

        /// <summary>
        /// group list by receiver name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupByReceiverName_Click(object sender, RoutedEventArgs e)
        {
            parcelsList.ItemsSource = from parcel in (IEnumerable<ListParcel>)parcelsList.ItemsSource
                                      orderby parcel.ReceiverName
                                      select parcel;
        }

        /// <summary>
        /// filters the list by state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// filters the list by sender name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboSender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// filters the list by receiver name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboReceiver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// filters the list by weight 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// filters the list by priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// filtes the list to be active after the date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            secondDate.DisplayDateStart = firstDate.SelectedDate;
            Refresh();
        }

        /// <summary>
        /// filtes the list to be active before the date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secondDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            firstDate.DisplayDateEnd = secondDate.SelectedDate;
            Refresh();
        }

        /// <summary>
        /// updates the list by the filters
        /// </summary>
        public void Refresh()
        {
            parcelsList.ItemsSource = ibl.GetFilteredParcelsList(
                firstDate.SelectedDate,
                secondDate.SelectedDate,
                comboSender.SelectedItem,
                comboReceiver.SelectedItem,
                comboPriority.SelectedItem,
                comboState.SelectedItem,
                comboWeight.SelectedItem);
        }
        #endregion

        #region Other Actions
        /// <summary>
        /// opens add parcel window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow pW = new ParcelWindow(ibl);
            pW.Owner = this;
            pW.Show();
        }

        /// <summary>
        /// closes the window if allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit == false)
                e.Cancel = true;
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
        /// opens a parcel window of the selected parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (parcelsList.Items.Count > 0)
            {
                ParcelWindow pW = new ParcelWindow((ListParcel)parcelsList.SelectedItem, ibl);
                pW.Owner = this;
                pW.Show();
            }
        }
        #endregion
    }
}
