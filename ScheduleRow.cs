using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBooking
{
    public class ScheduleRow
    {
        public string ID { get; set; }
        public string FlightNO { get; set; }
        public string Departing { get; set; }
        public string Arriving { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Economy { get; set; }
        public string Business { get; set; }
        public string First { get; set; }
        public string Duration { get; set; }
        public string DepartingName { get; set; }
        public string ArrivingName { get; set; }
        public string getString { get { return this.ToString(); } }
        public   override string ToString()
        {
            return $"From {this.DepartingName} flying to {this.ArrivingName} on: {Time}";
        }
        public ScheduleRow()
        {

        }

        public bool isValid()
        {
            if(
                this.Departing == null ||
                this.Arriving == null||
                this.Time == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void removeSeat(string seat) {
            int newSeatNumber = Convert.ToInt32(this.GetType().GetProperty(seat).GetValue(this)) -1;
            this.GetType().GetProperty(seat).SetValue(this, newSeatNumber.ToString());
        }
    }

  }
