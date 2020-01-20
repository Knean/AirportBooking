using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBooking
{
    class compareDepartures : IEqualityComparer<ScheduleRow>
    {
        public bool Equals(ScheduleRow x, ScheduleRow y)
        {
            if (x.Departing == y.Departing)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public int GetHashCode(ScheduleRow obj)
        {
            return obj.Departing.GetHashCode();
        }
    }

}


