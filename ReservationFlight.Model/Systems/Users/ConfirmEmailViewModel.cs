using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ConfirmEmailViewModel
    {
        public string Token { get; set; }

        public string Email { get; set; }
    }
}
