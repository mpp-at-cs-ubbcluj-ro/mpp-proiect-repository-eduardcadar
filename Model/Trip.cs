using System;

namespace Model
{
    [Serializable]
    public class Trip : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }

        public Trip() { }

        public Trip(string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            TouristAttraction = touristAttraction;
            TransportCompany = transportCompany;
            DepartureTime = departureTime;
            Price = price;
            Seats = seats;
        }
        public Trip(int id, string touristAttraction, string transportCompany, TimeSpan departureTime, float price, int seats)
        {
            Id = id;
            TouristAttraction = touristAttraction;
            TransportCompany = transportCompany;
            DepartureTime = departureTime;
            Price = price;
            Seats = seats;
        }

        public int getId()  
        {
            return Id;
        }

        public void setId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"Id={Id}, TouristAttraction={TouristAttraction}, TransportCompany={TransportCompany}," +
                $"DepartureTime={DepartureTime}, Price={Price}, Seats={Seats}";
        }
    }
}
