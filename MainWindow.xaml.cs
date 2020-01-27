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
        public Dictionary<RadioButton,string> radioButtons;
        ScheduleRow selectedItem;
        //object soon to be inserted into the datatable
        Reservation newReservation;
        public void disableBookingButton()
        {
            //disable the reservation button until all fields are valid
            createBooking.IsEnabled = newReservation.isValid();
            this.successMessage.Visibility = Visibility.Collapsed;
            this.successBorder.Visibility = Visibility.Collapsed;
        }
        public MainWindow()
        {          
            InitializeComponent();
            radioButtons = new Dictionary<RadioButton, string>()
            {
                { economyRadio,"Economy" },
                {businessRadio, "Business" },
                {firstRadio,"First" }
            };
            newReservation = new Reservation();
            scheduleRows = ConnectionObject.LoadScheduleRows();
            //never used???
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
            this.arrivingBox.ItemsSource = arrivalRows.Distinct(new compareArrivals());
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
                //disable radio buttons for seats that are sold out
                foreach (KeyValuePair<RadioButton, string> item in radioButtons)
                {
                    //PropertyInfro.GetValue method to access property stored in a variable
                    int seats = Convert.ToInt32(selectedItem.GetType().GetProperty(item.Value).GetValue(selectedItem, null));
                    item.Key.IsEnabled = seats == 0 ? false : true;                     
                }
                //update the Reservation object with the selected Time and ID
                newReservation.Time = selectedItem.Time;
                newReservation.FlightNo = selectedItem.ID;
            }   

        }


        private void seatTypeChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            //the value stored on the Reservation object is different from radio controls name
            switch (radio.Name)
            {
                case "economyRadio":
                    newReservation.SeatClass = "Economy";
                    break;
                case "businessRadio":
                    newReservation.SeatClass = "Business";
                    break;
                case "firstRadio":
                    newReservation.SeatClass = "First";
                    break;
            }
        }

        private void passengerName_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            newReservation.PassengerName = box.Text;
        }
        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            // pick the selected flight from the times combo box
            ScheduleRow flight = this.timeBox.SelectedItem as ScheduleRow;
            //updates the reservations table
            ConnectionObject.CreateBooking(newReservation);
            //updates the sql database and the local variable
            ConnectionObject.RemoveSeat(newReservation.SeatClass, flight);
 
            string successText =
                $"Success! You've booked {(newReservation.SeatClass == "Economy" ? "an" : "a")} {newReservation.SeatClass} seat on the flight {selectedItem} for {newReservation.PassengerName}" +
                $"{System.Environment.NewLine}Reference Number: {newReservation.Reference}";
            //reset the reservation object
            newReservation = new Reservation();
            //resubscribe to the new reservation object
            newReservation.changeChecker.changeMadeEvent += disableBookingButton;
            //reset the text boxes
            this.passengerName.Text = null;
            this.passportNo.Text = null;           
            this.passengerName.Text = null;
            this.departuresBox.SelectedItem = null;            
     
            //uncheck all seat class controls
            foreach (KeyValuePair<RadioButton, string> item in radioButtons)
            {
                item.Key.IsChecked = false;
               
            }
           //display the success message
            this.successMessage.Text = successText;
            
            this.successBorder.Visibility = Visibility.Visible;
            
            this.successMessage.Visibility = Visibility.Visible;
            this.UpdateLayout();
        }

        private void passwordEntered(object sender, RoutedEventArgs e)
        {
            //hash string for the password: seesharp
            string passwordHash = "1463063956";

            string inputHash = this.PasswordBox.Password.GetHashCode().ToString();
            //MessageBox.Show((password == secretPassword).ToString());
            this.PasswordBox.Password = null;
            if (inputHash == passwordHash){                
                Page flightsPage = new FlightsPage();
                this.Content = flightsPage;
            }
        }

        //attempt to log in if enter is pressed
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Enter)
            {
                this.passwordEntered(sender, e);
            }           
        }     

    }
}
