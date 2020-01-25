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
        //IEnumerable<ScheduleRow> rows;
        IEnumerable<ScheduleRow> departureRows;
        IEnumerable<ScheduleRow> arrivalRows;
        IEnumerable<ScheduleRow> listViewRows;
        List<Airport> airports;
        //Dictionary<string, string> airportDictionary;
        //List<string> hours;
        bool editMode;

        public FlightsPage()
        {

            InitializeComponent();
            airports = ConnectionObject.LoadAirports();
            //airportDictionary = new Dictionary<string, string>();
            //foreach (Airport airport in airports)
            //{
            //    airportDictionary.Add( airport.AirportCode, airport.Description);
            //}
            selectedItem = new ScheduleRow();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            editMode = false;

            //rows = from flight in scheduleRows
            //       select flight;
            arrivalRows = scheduleRows.Distinct(new compareArrivals());
            departureRows = scheduleRows.Distinct(new compareDepartures());
            listViewRows = from flight in scheduleRows
                           select flight;

            this.ArrivingBox.ItemsSource = airports;
            this.ArrivingBox.DisplayMemberPath = "Description";

            //this.DepartingBox.ItemsSource = departureRows;
            //this.DepartingBox.DisplayMemberPath = "Departing";
            this.DepartingBox.ItemsSource = airports;
            this.DepartingBox.DisplayMemberPath = "Description";

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
            timeColumn.Header = "Time";
            //timeColumn.Width = 100;
            timeColumn.DisplayMemberBinding = new Binding("Time");
            flightsGrid.Columns.Add(timeColumn);
            
       
            this.FlightsList.View = flightsGrid;            
            this.FlightsList.ItemsSource = scheduleRows; 
           
            
        }


        void filterRows()
        {

            // MessageBox.Show(this.selectedItem.Departing.ToString() + selectedItem.Arriving.ToString());

            //only departure is selected
            if (selectedItem.Departing != null && selectedItem.Arriving ==null && editMode ==false)
            {
                //pointlesss               
                listViewRows = listViewRows.Where(flight => flight.Departing == selectedItem.Departing);

         
            }
            //nothing is selected
            if(selectedItem.Departing == null && selectedItem.Arriving == null && editMode ==false)
            {
                
                listViewRows = from flight in scheduleRows
                               select flight;
            }

            // if departing and arriving is selected
            if (selectedItem.Departing != null && selectedItem.Arriving != null )
            {
                listViewRows = from ScheduleRow flight in scheduleRows
                               where flight.Departing == selectedItem.Departing &&
                               flight.Arriving == selectedItem.Arriving
                               select flight;
            }      
           

            if (editMode == true)
            {
                listViewRows = listViewRows.Where(flight => flight.ID == selectedItem.ID);
            }
                                 
            this.FlightsList.ItemsSource = listViewRows;
            if (editMode == true)
            {
                FlightsList.SelectedIndex = 0;
            }

        }


        private void ArrivingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            somethingChanged();            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            somethingChanged();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            listViewRows = from flight in scheduleRows
                           select flight;
            this.selectedItem = new ScheduleRow();          
            this.FlightsList.SelectedItem = null;
            this.DepartingBox.SelectedItem = null;
            this.ArrivingBox.SelectedItem = null;
            editMode = false;
            this.create.IsEnabled = false;
            this.update.IsEnabled = false;
            this.dateControl.SelectedDate = null;
            filterRows();
        }

       
        private void FlightsList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {           
            ListView listBox = sender as ListView;
            ScheduleRow selected = listBox.SelectedItem as ScheduleRow;
            if (selectedItem != null && selected != null)
            {
                //change status
                editMode = true;
                DateTime selectedHours = Convert.ToDateTime(selected.Time);
                this.dateControl.SelectedDate = selectedHours.Date;
                this.minutesBox.Text = selectedHours.Minute.ToString();
                this.hoursBox.Text = selectedHours.Hour.ToString();
                this.DepartingBox.SelectedIndex= airports.FindIndex(item => item.AirportCode == selected.Departing);
                this.ArrivingBox.SelectedIndex = airports.FindIndex(item => item.AirportCode == selected.Arriving);
                this.delete.IsEnabled = true;
                selectedItem.ID = selected.ID;
                selectedItem.Departing = selected.Departing;
                selectedItem.Arriving = selected.Arriving;
                selectedItem.Time = selected.Time;
                this.update.IsEnabled = true;
                this.create.IsEnabled = true;
                filterRows();
            }

        }

        private void hoursBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            somethingChanged();
        }

        private void minutesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            somethingChanged();
        }

        private void dateControl_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {         
          
            addDate();           

            filterRows();

        }

        private void addDate()
        {
            try
            {
                DateTime datestring = Convert.ToDateTime(this.dateControl.SelectedDate);
                datestring = datestring.AddHours(Convert.ToDouble(this.hoursBox.Text));
                datestring = datestring.AddMinutes(Convert.ToDouble(this.minutesBox.Text));
                selectedItem.Time = datestring.ToString();
                
               // MessageBox.Show(datestring.ToString());
            }
            catch{
                MessageBox.Show("enter valid date");
                
            }
        }

        private void somethingChanged()
        {
            Airport selected = new Airport();
            //departure
            if (selectedItem != null)
            {
                selected = DepartingBox.SelectedItem as Airport;
                selectedItem.Departing = selected?.AirportCode;
               
                //arrival
                selected = ArrivingBox.SelectedItem as Airport;
                selectedItem.Arriving = selected?.AirportCode;               

                //date + time
                addDate();

                //if the temporary object is valid
                if (selectedItem.isValid())
                {
                    this.create.IsEnabled = true;
                    bool selectedHasID= this.selectedItem.ID != null ? true : false; ;
                    this.update.IsEnabled = selectedHasID;
                    this.delete.IsEnabled = selectedHasID;
              
                }
                filterRows();                
            }         

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ConnectionObject.CreateFlight(this.selectedItem);
            scheduleRows = ConnectionObject.LoadScheduleRows();
            selectedItem = new ScheduleRow();
            editMode = false;
            somethingChanged();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ConnectionObject.UpdateFlight(this.selectedItem);
            scheduleRows = ConnectionObject.LoadScheduleRows();           
            selectedItem = new ScheduleRow();
            editMode = false;
            filterRows();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ConnectionObject.RemoveFlight(selectedItem);
            scheduleRows = ConnectionObject.LoadScheduleRows();            
            selectedItem = new ScheduleRow();
            editMode = false;
            FlightsList.SelectedItem = null;
            somethingChanged();          
  
        }
    }
}
