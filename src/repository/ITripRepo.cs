using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentiiDeTurism.src.domain;

namespace AgentiiDeTurism.src.repository
{
    public interface ITripRepo : IRepoInterface<Trip, int>
    {
        ICollection<Trip> getTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime);
    }
}
