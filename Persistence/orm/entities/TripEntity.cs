using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.orm.entities
{
    public class TripEntity
    {
        [Key]
        public int Id { get; set; }
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }
        public ICollection<ReservationEntity> Reservations { get; set; }

    }
}
