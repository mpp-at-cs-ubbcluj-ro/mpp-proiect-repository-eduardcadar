using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentiiDeTurism.src.domain;

namespace AgentiiDeTurism.src.repository
{
    public interface IAgencyRepo : IRepoInterface<Agency, int>
    {
        Agency getByName(string name);
    }
}
