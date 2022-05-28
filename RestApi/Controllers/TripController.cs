using Microsoft.AspNetCore.Mvc;
using Model;
using Persistence.orm.repos;

namespace RestApi.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripDbOrmRepo _tripRepo;

        public TripController(TripDbOrmRepo tripRepo)
        {
            _tripRepo = tripRepo;
        }

        [HttpPost]
        public ActionResult CreateTrip([FromBody] TripDto tripDto)
        {
            try
            {
                var trip = new Trip(tripDto.TouristAttraction, tripDto.TransportCompany,
                    TimeSpan.Parse(tripDto.DepartureTime), tripDto.Price, tripDto.Seats);
                var createdTrip = _tripRepo.Save(trip);
                return CreatedAtAction(nameof(GetTripById), new { id = createdTrip.Id }, createdTrip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult UpdateTrip(int id, [FromBody] TripDto tripDto)
        {
            try
            {
                var trip = new Trip(tripDto.TouristAttraction, tripDto.TransportCompany,
                    TimeSpan.Parse(tripDto.DepartureTime), tripDto.Price, tripDto.Seats);
                var changedTrip = _tripRepo.Update(id, trip);
                if (changedTrip == null)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Trip> DeleteTrip(int id)
        {
            try
            {
                var trip = _tripRepo.Delete(id);
                if (trip == null)
                    return NotFound();
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Trip> GetTripById(int id)
        {
            try
            {
                var trip = _tripRepo.GetById(id);
                if (trip == null)
                    return NotFound();
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Trip>> GetTrips([FromQuery(Name = "attraction")] string? touristAttraction,
            [FromQuery(Name = "start")] string? startTime, [FromQuery(Name = "end")] string? endTime)
        {
            try
            {
                if (touristAttraction == null)
                    touristAttraction = "";
                if (startTime == null)
                    startTime = "00:00:00";
                if (endTime == null)
                    endTime = "23:59:59";
                var startTimeSpan = TimeSpan.Parse(startTime);
                var endTimeSpan = TimeSpan.Parse(endTime);
                var trips = _tripRepo.GetTouristAttractionTrips(touristAttraction,
                    startTimeSpan, endTimeSpan);
                return Ok(trips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class TripDto
    {
        public string TouristAttraction { get; set; }
        public string TransportCompany { get; set; }
        public string DepartureTime { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }
    }
}