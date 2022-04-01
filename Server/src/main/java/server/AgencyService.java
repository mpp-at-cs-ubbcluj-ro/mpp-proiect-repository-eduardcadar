package server;

import model.domain.Agency;
import repository.IAgencyRepo;

import java.util.Collection;

public class AgencyService {
    private final IAgencyRepo repo;

    public AgencyService(IAgencyRepo repo) {
        this.repo = repo;
    }

    public Collection<Agency> getAllAgencies() {
        return repo.getAll();
    }

    public Agency getAgency(Agency agency) { return repo.getAgency(agency.getName(), agency.getPassword()); }
}
