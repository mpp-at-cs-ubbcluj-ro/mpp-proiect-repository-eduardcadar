using Microsoft.EntityFrameworkCore;
using Persistence.orm.entities;

namespace Persistence.orm
{
    public class AgenciesContext : DbContext
    {
        public DbSet<AgencyEntity> Agencies { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }

        public AgenciesContext(DbContextOptions op) : base(op)
        {
        }
    }
}
