using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.utils
{
    public class TripDTO
    {
        public int Id { get; set; }
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public double Price { get; set; }
        public int AvailableSeats { get; set; }

        public TripDTO(int id, String touristAttraction, String transportCompany, TimeSpan departureTime, double price, int availableSeats)
        {
            this.Id = id;
            this.TouristAttraction = touristAttraction;
            this.TransportCompany = transportCompany;
            this.DepartureTime = departureTime;
            this.Price = price;
            this.AvailableSeats = availableSeats;
        }

        public TripDTO(String touristAttraction, String transportCompany, TimeSpan departureTime, double price, int availableSeats)
        {
            this.TouristAttraction = touristAttraction;
            this.TransportCompany = transportCompany;
            this.DepartureTime = departureTime;
            this.Price = price;
            this.AvailableSeats = availableSeats;
        }
    }
}
