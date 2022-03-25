using System;

namespace AgentiiDeTurism.src.domain
{
    public class Trip : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }

        public Trip(string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            this.TouristAttraction = touristAttraction;
            this.TransportCompany = transportCompany;
            this.DepartureTime = departureTime;
            this.Price = price;
            this.Seats = seats;
        }
        public Trip(int id, string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            this.Id = id;
            this.TouristAttraction = touristAttraction;
            this.TransportCompany = transportCompany;
            this.DepartureTime = departureTime;
            this.Price = price;
            this.Seats = seats;
        }

        public int getId()  
        {
            return this.Id;
        }

        public void setId(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return this.TouristAttraction + " at " + this.DepartureTime;
        }
    }
}
