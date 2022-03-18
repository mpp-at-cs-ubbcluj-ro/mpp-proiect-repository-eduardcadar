import controller.MainSceneController;
import domain.Agency;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import service.AgencyService;
import service.ReservationService;
import service.Service;
import service.TripService;
import utils.Pair;
import domain.Reservation;
import domain.Trip;
import repository.*;

import java.io.FileReader;
import java.io.IOException;
import java.sql.Time;
import java.util.List;
import java.util.Properties;

public class Main extends Application {
    Scene mainScene;
    MainSceneController mainSceneController;
    Service service;

    public static void main(String[] args) {
        launch();
    }

    @Override
    public void start(Stage primaryStage) throws IOException {
        FXMLLoader fxmlMain = new FXMLLoader(Main.class.getResource("controller/mainScene.fxml"));
        initialize();

        mainScene = new Scene(fxmlMain.load());
        mainSceneController = fxmlMain.getController();
        mainSceneController.initialize(this.service);
        primaryStage.setScene(mainScene);
        primaryStage.show();

//        agencyRepo.save(new Agency("agentie", "parola"));
//        List<Agency> agencies = agencyRepo.getAll().stream().toList();
//        agencies.forEach(System.out::println);
//
//        Trip trip = new Trip("munte", "companie2", Time.valueOf("12:00:00"), 100.2, 20);
//        tripRepo.save(trip);
//        tripRepo.getTouristAttractionTrips("mare", Time.valueOf("11:00:00"), Time.valueOf("13:00:00"))
//                .forEach(System.out::println);
//        System.out.println();
//        tripRepo.getAll().forEach(System.out::println);
//
//        resRepo.save(new Reservation(new Pair<>("client", trip), "0722222222", 3));
//        System.out.println(resRepo.getById(new Pair<>("client", trip)));
//        resRepo.getAll().forEach(System.out::println);
    }

    private void initialize() {
        Properties props = new Properties();
        try {
            props.load(new FileReader("db.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config " + e);
        }

        AgencyRepo agencyRepo = new AgencyDBRepo(props);
        TripRepo tripRepo = new TripDBRepo(props);
        ReservationRepo resRepo = new ReservationDBRepo(props, tripRepo);

        AgencyService agencyService = new AgencyService(agencyRepo);
        TripService tripService = new TripService(tripRepo);
        ReservationService reservationService = new ReservationService(resRepo);

        Service srv = new Service(agencyService, tripService, reservationService);
        this.service = srv;
    }
}
