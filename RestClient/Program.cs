// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MainProgram
{
    class MainClass
    {
        static readonly HttpClient _client = new();

        public static void Main(string[] args)
        {
            Console.WriteLine("Start");
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = "https://localhost:7221/api/trips";

            // GETALL
            TripDTO[] trips = await GetTripsAsync(path);
            Console.WriteLine("Am primit: ");
            foreach (TripDTO t in trips)
                Console.WriteLine(t);

            // CREATE
            TripDTO trip = new()
            {
                TouristAttraction = "restAttraction",
                TransportCompany = "restCompany",
                DepartureTime = TimeSpan.Parse("17:10:00"),
                Price = 5,
                Seats = 10
            };
            await CreateTrip(path, trip);
            trips = await GetTripsAsync(path);
            Console.WriteLine("Am primit: ");
            foreach (TripDTO t in trips)
                Console.WriteLine(t);

            // GETBYID
            int id = trips.LastOrDefault().Id;
            var tripResult = await GetById(path, id);
            Console.WriteLine("GetById: " + tripResult);

            // UPDATE
            trip.TouristAttraction = "updatedRestAttraction";
            trip.TransportCompany = "updatedRestCompany";
            await UpdateTrip(path, id, trip);
            var updatedTrip = await GetById(path, id);
            Console.WriteLine("Updated trip: " + updatedTrip);

            // DELETE
            var tripDeleted = await DeleteTrip(path, id);
            Console.WriteLine("Deleted trip: " + tripDeleted);
            trips = await GetTripsAsync(path);
            Console.WriteLine("Am primit: ");
            foreach (TripDTO t in trips)
                Console.WriteLine(t);

            Console.ReadLine();
        }

        static async Task<TripDTO[]> GetTripsAsync(string path)
        {
            TripDTO[] result = null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<TripDTO[]>();
            return result;
        }

        static async Task CreateTrip(string path, TripDTO trip)
        {
            var createTripDto = new TripNoIdDto()
            {
                TouristAttraction = trip.TouristAttraction,
                TransportCompany = trip.TransportCompany,
                DepartureTime = trip.DepartureTime.ToString(),
                Price = trip.Price,
                Seats = trip.Seats
            };
            var json = JsonConvert.SerializeObject(createTripDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(path, stringContent);
            Console.WriteLine("Created trip: " + await response.Content.ReadAsStringAsync());
        }

        static async Task UpdateTrip(string path, int id, TripDTO trip)
        {
            var createTripDto = new TripNoIdDto()
            {
                TouristAttraction = trip.TouristAttraction,
                TransportCompany = trip.TransportCompany,
                DepartureTime = trip.DepartureTime.ToString(),
                Price = trip.Price,
                Seats = trip.Seats
            };
            var json = JsonConvert.SerializeObject(createTripDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(path + '/' + id, stringContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task<TripDTO> DeleteTrip(string path, int id)
        {
            TripDTO result = null;
            var response = await _client.DeleteAsync(path + '/' + id);
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<TripDTO>();
            return result;
        }

        static async Task<TripDTO> GetById(string path, int id)
        {
            TripDTO result = null;
            var response = await _client.GetAsync(path + '/' + id);
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<TripDTO>();
            return result;
        }
    }

    public record TripDTO
    {
        public int Id { get; set; }
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, TouristAttraction={TouristAttraction}, TransportCompany={TransportCompany}," +
                $"DepartureTime={DepartureTime}, Price={Price}, Seats={Seats}";
        }
    }

    public class TripNoIdDto
    {
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public string DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }
    }
}