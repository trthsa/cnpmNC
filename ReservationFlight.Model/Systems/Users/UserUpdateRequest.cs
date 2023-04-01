using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_NAME_USER)]
        public string Name { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_EMAIL_USER)]
        public string Email { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER)]
        public string PhoneNumber { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_ADDRESS_USER)]
        public string Address { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_USER_NAME_USER)]
        public string UserName { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER)]
        public string Password { get; set; }
    }
}
