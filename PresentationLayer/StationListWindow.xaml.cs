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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        List<string> range=new List<string>();
        public StationListWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
                range.Add(i.ToString());
            comboNumberSlots.ItemsSource = range;
        }

        private void comboNumberSlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstvDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void lstvStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
