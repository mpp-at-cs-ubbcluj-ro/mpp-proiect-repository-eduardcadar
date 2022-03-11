using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentiiDeTurism.src.domain;

namespace AgentiiDeTurism.src.repository
{
    public interface IReservationRepo : IRepoInterface<Reservation, Tuple<string, Trip>>
    {
    }
}
