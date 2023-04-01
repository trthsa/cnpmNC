using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ReservationFlight.Data.Entities
{
    // Guid là kiểu duy nhất cho toàn hệ thống
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}