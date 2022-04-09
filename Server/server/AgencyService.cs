using System;
using System.Collections.Generic;
using Model;
using Persistence;

namespace Server.server
{
    public class AgencyService
    {
        private readonly IAgencyRepo _repo;
        public AgencyService(IAgencyRepo repo)
        {
            _repo = repo;
        }
        public IEnumerable<Agency> GetAll() => _repo.GetAll();

        public Agency Get(Agency agency) => _repo.Get(agency.Name, agency.Password);
    }
}