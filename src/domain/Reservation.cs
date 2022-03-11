using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.domain
{
    public class Reservation : IIdentifiable<Tuple<string, Trip>>
    {
        public string client { get; set;  }
        public Trip trip { get; set; }
        public string phoneNumber { get; set; }
        public int seats { get; set; }

        public Reservation(Tuple<string, Trip> id, string phoneNumber, int seats)
        {
            this.client = id.Item1;
            this.trip = id.Item2;
            this.phoneNumber = phoneNumber;
            this.seats = seats;
        }

        public Tuple<string, Trip> getId()
        {
            return new Tuple<string, Trip>(this.client, this.trip);
        }

        public void setId(Tuple<string, Trip> id)
        {
            this.client = id.Item1;
            this.trip = id.Item2;
        }
    }
}
