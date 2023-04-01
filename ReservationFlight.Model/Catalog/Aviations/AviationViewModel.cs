using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace ReservationFlight.Model.Catalog.Aviations
{
    public class AviationViewModel
    {
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_AVIATION_CODE)]
        public string AviationCode { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_NAME_AVIATION)]
        public string Name { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_IMAGE_AVIATION)]
        public string ImageAviation { get; set; }

    }
}
