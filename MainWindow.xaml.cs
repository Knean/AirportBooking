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
        Reservation Reservation;
        public MainWindow()
        {
            
            InitializeComponent();
            Reservation = new Reservation();
            scheduleRows = new List<ScheduleRow>();
            using (var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Downloads\airplanes.accdb;" +
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
           
            //MessageBox.Show(selectedItem.Departing); 
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

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            AirportBooking.airplanesDataSet airplanesDataSet = ((AirportBooking.airplanesDataSet)(this.FindResource("airplanesDataSet")));
            // Load data into the table CyanairScheduleExtended. You can modify this code as needed.
            AirportBooking.airplanesDataSetTableAdapters.CyanairScheduleExtendedTableAdapter airplanesDataSetCyanairScheduleExtendedTableAdapter = new AirportBooking.airplanesDataSetTableAdapters.CyanairScheduleExtendedTableAdapter();
            airplanesDataSetCyanairScheduleExtendedTableAdapter.Fill(airplanesDataSet.CyanairScheduleExtended);
            System.Windows.Data.CollectionViewSource cyanairScheduleExtendedViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cyanairScheduleExtendedViewSource")));
            cyanairScheduleExtendedViewSource.View.MoveCurrentToFirst();
            // Load data into the table CyanairReservation. You can modify this code as needed.
            AirportBooking.airplanesDataSetTableAdapters.CyanairReservationTableAdapter airplanesDataSetCyanairReservationTableAdapter = new AirportBooking.airplanesDataSetTableAdapters.CyanairReservationTableAdapter();
            airplanesDataSetCyanairReservationTableAdapter.Fill(airplanesDataSet.CyanairReservation);
            System.Windows.Data.CollectionViewSource cyanairReservationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cyanairReservationViewSource")));
            cyanairReservationViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 page = new Page1(); 
            
            this.Content = page;
        }

        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            /*
            using (var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Downloads\airplanes.accdb;" +
                             "Persist Security Info = False;"))
            {
                conn.Open();
                command = new OleDbCommand("INSERT INTO CyanairReservation (Departing, Arriving, Time, [Seat Class], [Passenger Full Name]) " +
                    "VALUES (@Departing, @Arriving, @Time, @SeatClass, @PassengerName, @PassportNo)", conn);
                command.Parameters.AddWithValue("Departing", selectedItem.Departing);
                command.Parameters.AddWithValue("Arriving", selectedItem.Arriving);
                command.Parameters.AddWithValue("Time", selectedItem.Time);
                command.Parameters.AddWithValue("SeatClass", selectedItem.SeatClass);
                command.Parameters.Add(new OleDbParameter("PassengerName", txtName.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("INSERT INTO PassengerSeats (PassengerID, SeatID) " +
                    "SELECT MAX(PassengerID), 0 from Passengers", conn);
                command.ExecuteNonQuery();

                MessageBox.Show("Passenger " + txtName.Text + " was added to the waiting list",
                    "Waiting List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } */
        }

        private void EconomyRadio_Checked(object sender, RoutedEventArgs e)
        {
            Reservation.SeatClass = "Economy";
        }
    }
}
