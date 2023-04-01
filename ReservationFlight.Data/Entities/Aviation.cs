using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Data.Entities
{
    public class Aviation
    {
        public string AviationCode { get; set; }
        public string Name { get; set; }
        public string ImageAviation { get; set; }
        public int Status { get; set; }
        public List<FlightSchedule> FlightSchedules { get; set; }
    }
}
