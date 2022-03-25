using System;
using System.Collections.Generic;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.repository;

namespace AgentiiDeTurism.src.service
{
    public class AgencyService
    {
        private IAgencyRepo repo;
        public AgencyService(IAgencyRepo repo)
        {
            this.repo = repo;
        }
        public ICollection<Agency> getAllAgencies()
        {
            return repo.getAll();
        }

        public Agency getByName(String name) { return repo.getByName(name); }
    }
}