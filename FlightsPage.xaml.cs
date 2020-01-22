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
        public ScheduleRow selectedItem;
        IEnumerable<ScheduleRow> rows;


        public FlightsPage()
        {

            InitializeComponent();
            selectedItem = new ScheduleRow();
            scheduleRows = ConnectionObject.LoadScheduleRows();

            rows = from flight in scheduleRows
                   select flight;
            // where flight.Departing == selectedItem?.Departing
            



            this.ArrivingBox.ItemsSource = rows;
            this.ArrivingBox.DisplayMemberPath = "Arriving";

            this.DepartingBox.ItemsSource = rows;
            this.DepartingBox.DisplayMemberPath = "Departing";

            GridView flightsGrid = new GridView();
            flightsGrid.AllowsColumnReorder = true;
            flightsGrid.ColumnHeaderToolTip = "flight Data2";

            GridViewColumn departuresColumn = new GridViewColumn();
            departuresColumn.DisplayMemberBinding = new Binding("DepartingName");
            departuresColumn.Header = "Departing";
            departuresColumn.Width = 100;
            flightsGrid.Columns.Add(departuresColumn);

            GridViewColumn arrivalsColumn = new GridViewColumn();
            arrivalsColumn.Header = "Arriving";
            arrivalsColumn.Width = 100;
            arrivalsColumn.DisplayMemberBinding = new Binding("ArrivingName");
            flightsGrid.Columns.Add(arrivalsColumn);

            GridViewColumn timeColumn = new GridViewColumn();
            timeColumn.Header = "Arriving";
            //timeColumn.Width = 100;
            timeColumn.DisplayMemberBinding = new Binding("Time");
            flightsGrid.Columns.Add(timeColumn);
            
       
            this.FlightsList.View = flightsGrid;            
            this.FlightsList.ItemsSource = scheduleRows; 
           
            
        }


        void filterRows()
        {
            
           // MessageBox.Show(this.selectedItem.Departing.ToString() + selectedItem.Arriving.ToString());
            if (selectedItem.Departing != null)
            {
                rows = rows.Where(flight => flight.Departing == selectedItem.Departing);
            }

            if (selectedItem.Arriving != null)
            {
                MessageBox.Show(rows.FirstOrDefault().ToString()) ;
                rows = rows.Where(flight => flight.Arriving == selectedItem.Arriving);
            }
            this.ArrivingBox.ItemsSource = rows;
            this.DepartingBox.ItemsSource = rows;
        }
        private void FlightsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // MessageBox.Show(scheduleRows[0].Departing);
            // ListView listBox = sender as ListView;
            // ScheduleRow selected = listBox.SelectedItems[0] as ScheduleRow;
            //this.ArrivingBox.SelectedItem ="what";
            ScheduleRow selected = FlightsList.SelectedItem as ScheduleRow;
            this.dateControl.SelectedDate = Convert.ToDateTime(selected.Time);
        }

        private void ArrivingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ScheduleRow selected = ArrivingBox.SelectedItem as ScheduleRow;
            //MessageBox.Show(selected.Arriving);
            selectedItem.Arriving = null;
            selectedItem.Arriving = selected?.Arriving;
            MessageBox.Show(selectedItem.Arriving);
            filterRows();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleRow selected = DepartingBox.SelectedItem as ScheduleRow;
            selectedItem.Departing = selected.Departing;
            filterRows();
        }
    }
}
