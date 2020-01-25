using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows;
namespace AirportBooking
{
    class ConnectionObject
    {
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).FullName) + @"/airplanes2.accdb;" +
                              "Persist Security Info = False;";

        public static void CreateBooking(Reservation newReservation)
        {
            OleDbCommand command;
            using (var conn = new OleDbConnection(ConnectionObject.connectionString))
            {
                conn.Open();
                try
                {
                    command = new OleDbCommand("INSERT INTO CyanairReservation (Departing, Arriving, [Time], [Seat Class], [Passenger Full Name], [Passport No], [Booking reference], [Flight No]) " +
                    "VALUES (@Departing, @Arriving, @Time, @SeatClass, @PassengerName, @PassportNo, @Reference, @FlightIDVar)", conn);
                    command.Parameters.AddWithValue("Departing", newReservation.Departing);
                    command.Parameters.AddWithValue("Arriving", newReservation.Arriving);
                    command.Parameters.AddWithValue("Time", newReservation.Time);
                    command.Parameters.AddWithValue("SeatClass", newReservation.SeatClass);
                    command.Parameters.AddWithValue("PassengerName", newReservation.PassengerName);
                    command.Parameters.AddWithValue("PassportNo", "nopass");
                    command.Parameters.AddWithValue("Reference", newReservation.Reference);
                    command.Parameters.AddWithValue("FlightIDVar", newReservation.FlightNo);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        public static ScheduleRow RemoveSeat(string seatClass, ScheduleRow flight)
        {
            OleDbCommand command;
            int economy = Convert.ToInt32(flight.Economy);
            int business = Convert.ToInt32(flight.Business);
            int first = Convert.ToInt32(flight.First);

            switch (seatClass)
            {
                case "Economy":
                    flight.Economy = (Convert.ToInt32(flight.Economy) - 1).ToString();
                    break;
                case "Business":
                    flight.Business = (Convert.ToInt32(flight.Business) - 1).ToString();

                    break;
                case "First":
                    flight.First = (Convert.ToInt32(flight.First) - 1).ToString();
                    break;
            }
            using (var conn = new OleDbConnection(ConnectionObject.connectionString))
            {
                conn.Open();
                try
                {
                    command = new OleDbCommand("UPDATE CyanairScheduleExtended SET [Economy] = @economyVar, Business = @businessVar, [First] = @firstVar WHERE ID = @flightNoVar", conn);
                    command.Parameters.AddWithValue("economyVar", flight.Economy);
                    command.Parameters.AddWithValue("businessVar",flight.Business);
                    command.Parameters.AddWithValue("firstVar",flight.First);
                    command.Parameters.AddWithValue("flightNoVar", flight.ID);      

                    var mario = command.ExecuteNonQuery();
                    MessageBox.Show(mario.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            return flight;
        }

        public static List<ScheduleRow> LoadScheduleRows()
        {
            using (var conn = new OleDbConnection(ConnectionObject.connectionString))
            {
                List<ScheduleRow> scheduleRows = new List<ScheduleRow>();
                conn.Open();
                string selectionString = @"SELECT schedule1.* , air2.Descriptions as DepartingName, air3.Descriptions as ArrivingName
FROM((CyanairScheduleExtended schedule1
left join CyanairAirports air2 on air2.[Airport Codes] = schedule1.Departing)
left join CyanairAirports air3 on air3.[Airport Codes] = schedule1.Arriving)";
                OleDbCommand command = new OleDbCommand(selectionString, conn);
                OleDbDataReader reader = command.ExecuteReader();
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
                    scheduleRow.First = reader["First"].ToString();
                    scheduleRow.Duration = reader["Duration"].ToString();
                    
                    scheduleRow.ArrivingName = reader["ArrivingName"].ToString();
                    scheduleRow.DepartingName = reader["DepartingName"].ToString();
                    scheduleRows.Add(scheduleRow);

                }
                return scheduleRows;
            }
        }

        public static List<Airport> LoadAirports()
        {
            using (var conn = new OleDbConnection(ConnectionObject.connectionString))
            {
                List<Airport> Airports = new List<Airport>();
                conn.Open();
                OleDbCommand command = new OleDbCommand("SELECT * from CyanairAirports", conn);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Airport AirportItem = new Airport();
                    AirportItem.Description = reader["Descriptions"].ToString();
                    AirportItem.AirportCode = reader["Airport Codes"].ToString();
                    Airports.Add(AirportItem);

                }
                return Airports;
            }
        }
    }
}
