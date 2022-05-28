using Model;
using Persistence.orm.entities;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.orm.repos
{
    public class AgencyDbOrmRepo : IAgencyRepo
    {
        private readonly AgenciesContext _context;

        public AgencyDbOrmRepo(AgenciesContext context)
        {
            _context = context;
        }

        public Agency Get(string name, string password)
        {
            var agencyEntity = _context.Agencies.SingleOrDefault(
                a => a.Name == name && a.Password == password);
            var agency = EntityUtils.AgencyEntityToAgency(agencyEntity);
            return agency;
        }

        public IEnumerable<Agency> GetAll()
        {
            var agencyEntities = _context.Agencies.ToArray();
            IEnumerable<Agency> agencies = agencyEntities.Select(a => EntityUtils.AgencyEntityToAgency(a));
            return agencies;
        }

        public Agency GetById(string id)
        {
            var agencyEntity = _context.Agencies.Find(id);
            var agency = EntityUtils.AgencyEntityToAgency(agencyEntity);
            return agency;
        }

        public Agency Save(Agency elem)
        {
            var agencyEntity = EntityUtils.AgencyToAgencyEntity(elem);
            _context.Agencies.Add(agencyEntity);
            _context.SaveChanges();
            return EntityUtils.AgencyEntityToAgency(agencyEntity);
        }
    }
}
