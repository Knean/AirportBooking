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
        IEnumerable<ScheduleRow> departureRows;
        IEnumerable<ScheduleRow> arrivalRows;
        IEnumerable<ScheduleRow> listViewRows;
        List<string> hours;
        bool editMode;

        public FlightsPage()
        {

            InitializeComponent();
            selectedItem = new ScheduleRow();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            editMode = false;
            //hours = new List<string> { "01",""
            //this.hoursBox.ItemsSource = hours;
            

            rows = from flight in scheduleRows
                   select flight;
            arrivalRows = scheduleRows.Distinct(new compareArrivals());
            departureRows = scheduleRows.Distinct(new compareDepartures());
            listViewRows = from flight in scheduleRows
                           select flight;
            // where flight.Departing == selectedItem?.Departing




            this.ArrivingBox.ItemsSource = arrivalRows;
            this.ArrivingBox.DisplayMemberPath = "Arriving";

            this.DepartingBox.ItemsSource = departureRows;
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


            if (selectedItem.Departing != null && selectedItem.Arriving ==null && editMode ==false)
            {
                departureRows = rows.Where(flight => flight.Departing == selectedItem.Departing);
                listViewRows = listViewRows.Where(flight => flight.Departing == selectedItem.Departing);

                // MessageBox.Show("only flights with departure: " + selectedItem.Departing);
                ////departureRows = rows.Where(flight => flight.Departing == selectedItem.Departing);
                //listViewRows = listViewRows.Where(flight => flight.Departing == selectedItem.Departing);
                //arrivalRows = scheduleRows.Distinct(new compareArrivals());
                //if (selectedItem.Arriving == null)
                //{
                //    arrivalRows = rows.Where(flight => flight.Departing == selectedItem.Departing);
                //}
                //else
                //{
                //    listViewRows = listViewRows.Where(flight => flight.Departing == selectedItem.Departing);
                //}
            }
            if(selectedItem.Departing == null && selectedItem.Arriving == null && editMode ==false)
            {
                departureRows = scheduleRows.Distinct(new compareDepartures());
                arrivalRows = scheduleRows.Distinct(new compareArrivals());
                listViewRows = from flight in scheduleRows
                               select flight;
            }
            if (selectedItem.Departing != null && selectedItem.Arriving != null && editMode == false)
            {
                listViewRows = listViewRows.Where(flight => flight.Departing == selectedItem.Departing && flight.Arriving == selectedItem.Arriving);
            }
            // if we hacve arriving value
            if (selectedItem.Arriving != null && selectedItem.Departing == null && editMode == false)
            {
                arrivalRows = rows.Where(flight => flight.Arriving == selectedItem.Arriving);
                listViewRows = listViewRows.Where(flight => flight.Arriving == selectedItem.Arriving);
                //// MessageBox.Show("only flights with arrival: " + selectedItem.Arriving ) ;
                ////arrivalRows = rows.Where(flight => flight.Arriving == selectedItem.Arriving);
                //departureRows = scheduleRows.Distinct(new compareDepartures());
                //listViewRows = listViewRows.Where(flight => flight.Arriving == selectedItem.Arriving);
                //// departureRows = listViewRows.Where(flight => flight.Arriving == selectedItem.Arriving);

                ////if we have arriving value but not departing value
                if (selectedItem.Departing == null)
                {
                    departureRows= rows.Where(flight => flight.Arriving == selectedItem.Arriving);
                }
                else
                {

                }



            }

            if (editMode == true)
            {
                listViewRows = listViewRows.Where(flight => flight.ID == selectedItem.ID);
            }



            this.ArrivingBox.ItemsSource = arrivalRows;
          
            this.DepartingBox.ItemsSource = departureRows;//.Distinct(new compareDepartures());
            this.FlightsList.ItemsSource = listViewRows;
            if (editMode == true)
            {
                FlightsList.SelectedIndex = 0;
            }

        }


        private void ArrivingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //ScheduleRow selected = ArrivingBox.SelectedItem as ScheduleRow;
            //selectedItem.Arriving = selected?.Arriving;            
            //filterRows();
            //this.DepartingBox.SelectedIndex = 2; //
            somethingChanged();
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ScheduleRow selected = DepartingBox.SelectedItem as ScheduleRow;
            //selectedItem.Departing = selected?.Departing;
            //filterRows();
            somethingChanged();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            listViewRows = from flight in scheduleRows
                           select flight;
            this.selectedItem = new ScheduleRow();

           // this.DepartingBox.SelectedItem = DepartingBox.Items ;
            this.FlightsList.SelectedItem = null;
            this.DepartingBox.SelectedItem = null;
            this.ArrivingBox.SelectedItem = null;
            editMode = false;
            this.create.IsEnabled = false;
            this.update.IsEnabled = false;
            filterRows();
        }

       
        private void FlightsList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(scheduleRows[0].Departing);
            ListView listBox = sender as ListView;
            ScheduleRow selected = listBox.SelectedItem as ScheduleRow;
            if (selectedItem != null && selected != null)
            {
                //change status
                editMode = true;
                DateTime selectedHours = Convert.ToDateTime(selected.Time);
                //MessageBox.Show(selectedHours.Minute.ToString());
                this.minutesBox.Text = selectedHours.Minute.ToString();
                this.hoursBox.Text = selectedHours.Hour.ToString();
                //this.ArrivingBox.SelectedItem ="what";
                // ScheduleRow selected = FlightsList.SelectedItem as ScheduleRow;
                this.dateControl.SelectedDate = Convert.ToDateTime(selected.Time);
               this.DepartingBox.SelectedIndex= departureRows.ToList().FindIndex(item => item.Departing == selected.Departing);
                this.ArrivingBox.SelectedIndex = arrivalRows.ToList().FindIndex(item => item.Arriving == selected.Arriving);
                this.delete.IsEnabled = true;
                selectedItem = selected;
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
            // MessageBox.Show("itworked");
          
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
            ScheduleRow selected = new ScheduleRow();
            //departure
            if (selectedItem != null)
            {
                selected = DepartingBox.SelectedItem as ScheduleRow;
                selectedItem.Departing = selected?.Departing;
               
                //arrival
                selected = ArrivingBox.SelectedItem as ScheduleRow;
                selectedItem.Arriving = selected?.Arriving;               

                //date + time
                addDate();

                //if the temporary object is valid
                if (selectedItem.isValid())
                {
                    this.create.IsEnabled = true;

                    if(this.selectedItem.ID != null)
                    {
                        this.update.IsEnabled = true;
                    }
                }
                filterRows();
            }
          

        }
    }
}
