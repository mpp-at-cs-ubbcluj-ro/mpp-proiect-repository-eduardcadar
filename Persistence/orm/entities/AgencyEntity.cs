using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.orm.entities
{
    public class AgencyEntity
    {
        [Key]
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<ReservationEntity> reservations { get; set; }
    }
}
