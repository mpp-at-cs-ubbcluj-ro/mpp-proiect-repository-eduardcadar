using System;

namespace AgentiiDeTurism.src.domain
{
    public class Reservation : IIdentifiable<Tuple<string, Trip>>
    {
        public string Client { get; set;  }
        public Trip Trip { get; set; }
        public string PhoneNumber { get; set; }
        public int Seats { get; set; }

        public Reservation(Tuple<string, Trip> id, string phoneNumber, int seats)
        {
            this.Client = id.Item1;
            this.Trip = id.Item2;
            this.PhoneNumber = phoneNumber;
            this.Seats = seats;
        }

        public Tuple<string, Trip> getId()
        {
            return new Tuple<string, Trip>(this.Client, this.Trip);
        }

        public void setId(Tuple<string, Trip> id)
        {
            this.Client = id.Item1;
            this.Trip = id.Item2;
        }

        public override string ToString()
        {
            return "Reservation by " + this.Client + " on trip to " + this.Trip.TouristAttraction + " for " + this.Seats + " seats";
        }
    }
}
