using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Utility
{
    public class ReservationFlightException : Exception
    {
        public ReservationFlightException()
        {
        }

        public ReservationFlightException(string message)
            : base(message)
        {
        }

        public ReservationFlightException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
