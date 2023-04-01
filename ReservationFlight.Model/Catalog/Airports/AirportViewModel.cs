using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Airports
{
    public class AirportViewModel
    {

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_IATA_AIRPORT)]
        public string IATA { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_NAME_AIRPORT)]
        public string Name { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_STATUS_AIRPORT)]
        public int Status { get; set; }
    }
}
