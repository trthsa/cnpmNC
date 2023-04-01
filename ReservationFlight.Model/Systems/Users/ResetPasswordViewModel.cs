using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ResetPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_CONFIRM_PASSWORD_USER)]
        [Compare(
            "Password", 
            ErrorMessage = Constants.DISPLAY_ATTRIBUTE_CONFIRM_PASSWORD_USER)]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
