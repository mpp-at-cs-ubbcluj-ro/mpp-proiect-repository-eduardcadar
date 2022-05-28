namespace Persistence.orm.entities
{
    public class ReservationEntity
    {
        public string Client { get; set; }
        public int TripId { get; set; }
        public TripEntity Trip { get; set; }
        public string AgencyId { get; set; }
        public AgencyEntity Agency { get; set; }
        public string PhoneNumber { get; set; }
        public int Seats { get; set; }
    }
}
