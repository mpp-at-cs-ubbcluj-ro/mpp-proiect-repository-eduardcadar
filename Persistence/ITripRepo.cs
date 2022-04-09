using System;
using System.Collections.Generic;
using Model;

namespace Persistence
{
    public interface ITripRepo : IRepoInterface<Trip, int>
    {
        IEnumerable<Trip> GetTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime);
    }
}
