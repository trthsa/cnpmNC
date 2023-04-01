using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();

    }
}
