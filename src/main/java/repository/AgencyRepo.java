package repository;

import domain.Agency;

public interface AgencyRepo extends RepoInterface<Agency, Integer> {
    Agency getByName(String name);
}
