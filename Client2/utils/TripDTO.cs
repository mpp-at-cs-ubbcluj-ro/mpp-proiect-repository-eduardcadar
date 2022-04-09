using System;

namespace Client2.utils
{
    [Serializable]
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
            Id = id;
            TouristAttraction = touristAttraction;
            TransportCompany = transportCompany;
            DepartureTime = departureTime;
            Price = price;
            AvailableSeats = availableSeats;
        }

        public TripDTO(String touristAttraction, String transportCompany, TimeSpan departureTime, double price, int availableSeats)
        {
            TouristAttraction = touristAttraction;
            TransportCompany = transportCompany;
            DepartureTime = departureTime;
            Price = price;
            AvailableSeats = availableSeats;
        }
    }
}
