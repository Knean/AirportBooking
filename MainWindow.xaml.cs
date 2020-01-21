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
using System.Data.OleDb;
namespace AirportBooking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OleDbCommand command;
        public OleDbDataReader reader;
        public static List<ScheduleRow> scheduleRows;
        public static List<Airport> AirportRows;
        ScheduleRow selectedItem;
        Reservation newReservation;
        public MainWindow()
        {           
            
            InitializeComponent();
            newReservation = new Reservation();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            AirportRows = ConnectionObject.LoadAirports();

            // subscribe to changes to the reservation object
            newReservation.changeChecker.changeMadeEvent += new ChangeChecker.changeMade(delegate ()
            {
                //disable the reservation button until all fields are valid
                createBooking.IsEnabled = newReservation.isValid();
            });
            /*
            var innerJoinQuery =
    from schedule in scheduleRows
    join airport in AirportRows on   schedule.dep equals airport.AirportCode
    select new { ProductName = prod.Name, Category = category.Name }; */

            this.departuresBox.ItemsSource = scheduleRows.Distinct(new compareDepartures());
            this.departuresBox.DisplayMemberPath = "DepartingName";         

            
        }
  
        private void DeparturesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleRow selectedItem =(ScheduleRow) this.departuresBox.SelectedItem;
            
            //filter the scheduleRows
            IEnumerable <ScheduleRow> arrivalRows = from flight in scheduleRows
                                                   where flight.Departing == selectedItem.Departing
                                                   select flight;

            this.arrivingBox.ItemsSource = arrivalRows;
            this.arrivingBox.DisplayMemberPath = "ArrivingName";
            
            newReservation.Departing = selectedItem.Departing;
            
        }

        private void ArrivingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ScheduleRow selectedItem = (ScheduleRow)this.arrivingBox.SelectedItem;
            IEnumerable < ScheduleRow > timeRows = new ScheduleRow[] { };
            if (selectedItem != null )
            {
               timeRows = from flight in scheduleRows
                                                    where flight.Departing == selectedItem.Departing
                                                    where flight.Arriving == selectedItem.Arriving
                                                    select flight;
            }
            
            this.timeBox.ItemsSource = timeRows;
            this.timeBox.DisplayMemberPath = "Date";
           
            newReservation.Arriving = selectedItem?.Arriving;
            
        }

        private void TimeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBox.Show(this.timeBox.Text);
            if (this.timeBox.SelectedItem != null)
            {
                selectedItem = (ScheduleRow)this.timeBox.SelectedItem;
                // disable buttons for unavailable seats
                int businessSeats = Convert.ToInt32(selectedItem.Business);
                this.businessRadio.IsEnabled = businessSeats < 0 ? false : true;

                int economySeats = Convert.ToInt32(selectedItem.Economy);
                this.economyRadio.IsEnabled = economySeats < 0 ? false : true;

                int firstSeats = Convert.ToInt32(selectedItem.Economy);
                this.economyRadio.IsEnabled = firstSeats < 0 ? false : true;
                newReservation.Time = selectedItem.Time;
            }
   

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 page = new Page1(); 
            
            this.Content = page;
        }
        private void EconomyRadio_Checked(object sender, RoutedEventArgs e)
        {
            newReservation.SeatClass = "Economy";
        }
    
        private void businessRadio_Checked(object sender, RoutedEventArgs e)
        {
            newReservation.SeatClass = "Business";
        }

        private void firstRadio_Checked(object sender, RoutedEventArgs e)
        {
            newReservation.SeatClass = "FirstClass";
        }
        private void passengerName_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            newReservation.PassengerName = box.Text;
        }
        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            ConnectionObject.CreateBooking(newReservation);
            this.passengerName.Text = null;
            this.passportNo.Text = null;

        }

    }
}
