using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.orm.entities
{
    public class ReservationEntity
    {
        [Key]
        [Column(Order = 1)]
        public string Client { get; set; }
        [Key]
        [Column(Order = 2)]
        public int TripId { get; set; }
        public TripEntity Trip { get; set; }
        public AgencyEntity Agency { get; set; }
        public string PhoneNumber { get; set; }
        public int Seats { get; set; }
    }
}
