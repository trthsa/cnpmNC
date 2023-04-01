using Microsoft.AspNetCore.Http;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservationFlight.Model.Catalog.Aviations
{
    public class AviationCreateRequest
    {
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_AVIATION_CODE)]
        public string AviationCode { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_NAME_AVIATION)]
        public string Name { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_IMAGE_AVIATION)]
        public IFormFile ImageAviation { get; set; }
    }
}
