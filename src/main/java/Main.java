import domain.Agency;
import repository.AgencyDBRepo;
import repository.AgencyRepo;

import java.io.FileReader;
import java.io.IOException;
import java.util.List;
import java.util.Properties;

public class Main {
    public static void main(String[] args) {
        Properties props = new Properties();
        try {
            props.load(new FileReader("db.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config " + e);
        }

        AgencyRepo repo = new AgencyDBRepo(props);
        repo.save(new Agency("agentie", "parola"));
        List<Agency> agencies = repo.getAll().stream().toList();
        agencies.forEach(System.out::println);
    }
}
