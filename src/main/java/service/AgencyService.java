package service;

import domain.Agency;
import repository.AgencyRepo;

import java.util.Collection;

public class AgencyService {
    private AgencyRepo repo;

    public AgencyService(AgencyRepo repo) {
        this.repo = repo;
    }

    public Collection<Agency> getAllAgencies() {
        return repo.getAll();
    }

    public Agency getByName(String name) { return repo.getByName(name); }
}
