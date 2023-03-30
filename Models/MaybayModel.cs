using System.ComponentModel.DataAnnotations;

namespace BanVeFlightAPI.Models
{
    public class MaybayModel
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
