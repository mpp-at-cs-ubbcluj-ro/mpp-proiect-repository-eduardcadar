package repository;

import model.domain.Agency;

public interface IAgencyRepo extends IRepo<Agency, String> {
    Agency getAgency(String name, String password);
}
