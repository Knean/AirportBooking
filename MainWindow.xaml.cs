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
        //object soon to be inserted into the datatable
        Reservation newReservation;
        public void disableBookingButton()
        {
            //disable the reservation button until all fields are valid
            createBooking.IsEnabled = newReservation.isValid();
        }
        public MainWindow()
        {          
            InitializeComponent();
            newReservation = new Reservation();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            AirportRows = ConnectionObject.LoadAirports();
            // subscribe to changes to the reservation object
            newReservation.changeChecker.changeMadeEvent += disableBookingButton;
            //
            //newReservation.changeChecker.changeMadeEvent += new ChangeChecker.changeMade(delegate ()
            //{
            //    
            //    createBooking.IsEnabled = newReservation.isValid();
            //});           
            // get flights with distinct departures
            this.departuresBox.ItemsSource = scheduleRows.Distinct(new compareDepartures());
            this.departuresBox.DisplayMemberPath = "DepartingName";
        }
  
        private void DeparturesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleRow selectedItem =(ScheduleRow) this.departuresBox.SelectedItem;            
            //find flights with matching departure field
            IEnumerable <ScheduleRow> arrivalRows = from flight in scheduleRows
                                                   where flight.Departing == selectedItem?.Departing
                                                   select flight;
            this.arrivingBox.ItemsSource = arrivalRows;
            this.arrivingBox.DisplayMemberPath = "ArrivingName";            
            newReservation.Departing = selectedItem?.Departing;            
        }

        private void ArrivingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleRow selectedItem = (ScheduleRow)this.arrivingBox.SelectedItem;
            //find flights with matching departure and arrival fields
            IEnumerable < ScheduleRow > timeRows = new ScheduleRow[] { };
         
               timeRows = from flight in scheduleRows
                                                    where flight.Departing == selectedItem?.Departing
                                                    where flight.Arriving == selectedItem?.Arriving
                                                    select flight;
                       
            this.timeBox.ItemsSource = timeRows;
            this.timeBox.DisplayMemberPath = "Date";           
            newReservation.Arriving = selectedItem?.Arriving;
            
        }

        private void TimeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            if (this.timeBox.SelectedItem != null)
            {
                selectedItem = (ScheduleRow)this.timeBox.SelectedItem;
                // disable radio buttons for unavailable seats
                int businessSeats = Convert.ToInt32(selectedItem.Business);
                this.businessRadio.IsEnabled = businessSeats ==0 ? false : true;

                int economySeats = Convert.ToInt32(selectedItem.Economy);
                this.economyRadio.IsEnabled = economySeats ==0 ?false : true;

                int firstSeats = Convert.ToInt32(selectedItem.First);
                this.firstRadio.IsEnabled = firstSeats ==0 ? false : true;
                newReservation.Time = selectedItem.Time;
                newReservation.FlightNo = selectedItem.ID;
            }   

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
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
            ScheduleRow flight = this.timeBox.SelectedItem as ScheduleRow;
            ConnectionObject.CreateBooking(newReservation);
            ConnectionObject.RemoveSeat(newReservation.SeatClass, flight);
            
            newReservation = new Reservation();
            newReservation.changeChecker.changeMadeEvent += disableBookingButton;
            //reset the form fields
            this.passengerName.Text = null;
            this.passportNo.Text = null;           
            this.passengerName.Text = null;
            this.departuresBox.SelectedItem = null;

            //put them in a list do stuff for each of them

            this.economyRadio.IsChecked = false;
            this.businessRadio.IsChecked = false;
            this.firstRadio.IsChecked = false;
            
            //connectionobject.removeseat(reservation)
        }

        private void passwordEntered(object sender, RoutedEventArgs e)
        {
            //password is seesharp
            string secretPassword = "1463063956";

            string password = this.PasswordBox.Password.GetHashCode().ToString();
            //MessageBox.Show((password == secretPassword).ToString());
            this.PasswordBox.Password = null;
            if (password == secretPassword){                
                Page flightsPage = new FlightsPage();
                this.Content = flightsPage;
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Enter)
            {
                this.passwordEntered(sender, e);
            }
           
        }
        // everything under this is flight schedule logic

    }
}
