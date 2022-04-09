using System;

namespace Model
{
    [Serializable]
    public class Reservation : IIdentifiable<Tuple<string, Trip>>
    {
        public string Client { get; set;  }
        public Trip Trip { get; set; }
        public string Agency { get; set; }
        public string PhoneNumber { get; set; }
        public int Seats { get; set; }

        public Reservation(Tuple<string, Trip> id, string agency, string phoneNumber, int seats)
        {
            Client = id.Item1;
            Trip = id.Item2;
            Agency = agency;
            PhoneNumber = phoneNumber;
            Seats = seats;
        }

        public Tuple<string, Trip> getId()
        {
            return new Tuple<string, Trip>(Client, Trip);
        }

        public void setId(Tuple<string, Trip> id)
        {
            Client = id.Item1;
            Trip = id.Item2;
        }

        public override string ToString()
        {
            return "Reservation by " + Client + " on trip to " + Trip.TouristAttraction + " for " + Seats + " seats";
        }
    }
}
