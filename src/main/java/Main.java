import controller.LoginSceneController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import repository.*;
import service.AgencyService;
import service.ReservationService;
import service.Service;
import service.TripService;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class Main extends Application {
    Scene loginScene;
    LoginSceneController loginSceneController;
    Service service;

    public static void main(String[] args) {
        launch();
    }

    @Override
    public void start(Stage primaryStage) throws IOException {
        initialize();

        FXMLLoader fxmlMain = new FXMLLoader(Main.class.getResource("controller/loginScene.fxml"));
        loginScene = new Scene(fxmlMain.load());
        loginSceneController = fxmlMain.getController();
        loginSceneController.initialize(this.service);
        primaryStage.setScene(loginScene);
        primaryStage.show();
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

        this.service = new Service(agencyService, tripService, reservationService);
    }
}
