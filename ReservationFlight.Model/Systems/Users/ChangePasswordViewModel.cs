using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_CURRENT_PASSWORD_USER)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_NEW_PASSWORD_USER)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_CONFIRM_PASSWORD_USER)]
        public string ConfirmPassword { get; set; }
    }
}
