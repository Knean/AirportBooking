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
        ScheduleRow selectedItem;
        Reservation newReservation;
        public MainWindow()
        {
            
            
            InitializeComponent();
            newReservation = new Reservation();
            
          // subscribe to changes to the reservation object and check if it's valid
            newReservation.changeChecker.changeMadeEvent += new ChangeChecker.changeMade(delegate ()
            {
                createBooking.IsEnabled = newReservation.isValid();
            }); ;
          


          
            scheduleRows = new List<ScheduleRow>();
            using (var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetParent(System.IO.Directory.GetParent( System.Environment.CurrentDirectory).FullName) + @"/airplanes2.accdb;" +
                              "Persist Security Info = False;"))
            {
                conn.Open();
                command = new OleDbCommand("SELECT * from CyanairScheduleExtended", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {                    
                    ScheduleRow scheduleRow = new ScheduleRow();
                    scheduleRow.ID = reader["id"].ToString();
                    scheduleRow.Departing = (string)reader["Departing"];
                    scheduleRow.Arriving = (string)reader["Arriving"];
                    scheduleRow.Date = reader["Date"].ToString();
                    scheduleRow.Time = reader["Time"].ToString();
                    scheduleRow.Economy = reader["Economy"].ToString();
                    scheduleRow.Business = reader["Business"].ToString();
                    scheduleRow.Duration = reader["Duration"].ToString();
                    scheduleRows.Add(scheduleRow);
                 
                }

                this.departuresBox.ItemsSource = scheduleRows.Distinct(new compareDepartures());
                this.departuresBox.DisplayMemberPath = "Departing";
               // IEnumerable<ScheduleRow> arrivalRows = from flight in scheduleRows where flight.Departing == departuresBox.SelectedItem
                
             
                /*                             
                 foreach (ScheduleRow flight in scheduleRows)
                {
                    MessageBox.Show(flight.Departing+ flight.Duration + flight.Time);
                }*/


            }
        }
  
        private void DeparturesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleRow selectedItem =(ScheduleRow) this.departuresBox.SelectedItem;

            IEnumerable <ScheduleRow> arrivalRows = from flight in scheduleRows
                                                   where flight.Departing == selectedItem.Departing
                                                   select flight;

            this.arrivingBox.ItemsSource = arrivalRows;
            this.arrivingBox.DisplayMemberPath = "Arriving";
            
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
            newReservation.Arriving = selectedItem.Arriving;
        }

        private void TimeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (ScheduleRow)this.timeBox.SelectedItem;
            // disable buttons for unavailable seats
            int businessSeats = Convert.ToInt32( selectedItem.Business);
            this.businessRadio.IsEnabled = businessSeats < 0 ? false : true;

            int economySeats = Convert.ToInt32(selectedItem.Economy);
            this.economyRadio.IsEnabled = economySeats < 0 ? false :  true;

            int firstSeats = Convert.ToInt32(selectedItem.Economy);
            this.economyRadio.IsEnabled = firstSeats < 0 ? false : true;
            newReservation.Time = selectedItem.Time;

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 page = new Page1(); 
            
            this.Content = page;
        }

        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).FullName) + @"/airplanes2.accdb;" +
                                "Persist Security Info = False;"))
            {
                conn.Open();
                try
                {
                    //command = new OleDbCommand("INSERT INTO CyanairReservation (Departing, Arriving, [Time], [Seat Class], [Passenger Full Name], [Passport No]) " +
                    //"VALUES (@Departing, @Arriving, @Time, @SeatClass, @PassengerName, @PassportNo)", conn);
                    command = new OleDbCommand(@"INSERT INTO CyanairReservation ( Departing, Arriving, [Time], [Seat Class] )
VALUES('test', 'test', 'test', 'test'); ", conn);
                  
                    command.ExecuteNonQuery();
                }
     catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

              
            } 
        }

        private void EconomyRadio_Checked(object sender, RoutedEventArgs e)
        {
            newReservation.SeatClass = "Economy";
        }

        private void passengerName_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            newReservation.PassengerName = box.Text;
        }
    }
}
