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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportBooking
{
    /// <summary>
    /// Interaction logic for FlightsPage.xaml
    /// </summary>
    /// 

    public partial class FlightsPage : Page
    {
        public static List<ScheduleRow> scheduleRows;
        public FlightsPage()
        {
            InitializeComponent();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            this.FlightsList.ItemsSource = scheduleRows;
            this.ArrivingBox.ItemsSource = scheduleRows;
            this.FlightsList.DisplayMemberPath = "Departing";
            
        }

        private void FlightsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(scheduleRows[0].Departing);
           // ListView listBox = sender as ListView;
           // ScheduleRow selected = listBox.SelectedItems[0] as ScheduleRow;
            this.ArrivingBox.SelectedItem ="what";
        }
    }
}
