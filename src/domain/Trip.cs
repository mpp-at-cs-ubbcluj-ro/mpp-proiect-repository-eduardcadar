using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.domain
{
    public class Trip : IIdentifiable<int>
    {
        public int id { get; set; }
        public string touristAttraction { get; set; }
        public string transportCompany { get; set; }
        public TimeSpan departureTime { get; set; }
        public float price { get; set; }
        public int seats { get; set; }

        public Trip(string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            this.touristAttraction = touristAttraction;
            this.transportCompany = transportCompany;
            this.departureTime = departureTime;
            this.price = price;
            this.seats = seats;
        }
        public Trip(int id, string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            this.id = id;
            this.touristAttraction = touristAttraction;
            this.transportCompany = transportCompany;
            this.departureTime = departureTime;
            this.price = price;
            this.seats = seats;
        }

        public int getId()  
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }
}
