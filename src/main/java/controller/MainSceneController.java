package controller;

import domain.Agency;
import domain.Reservation;
import domain.Trip;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import service.Service;
import utils.MyAlert;
import utils.TripDTO;

import java.io.IOException;
import java.sql.Time;
import java.util.HashSet;
import java.util.Set;


public class MainSceneController {
    private Service srv;
    private Agency loggedAgency;

    @FXML
    AnchorPane anchorPane;

    @FXML
    Label agencyLabel;

    @FXML
    ListView<Agency> listAgencies;

    @FXML
    TableView<TripDTO> tableTrips;
    @FXML
    TableColumn<TripDTO, String> columnTripAttraction;
    @FXML
    TableColumn<TripDTO, String> columnTripCompany;
    @FXML
    TableColumn<TripDTO, Time> columnTripDepartureTime;
    @FXML
    TableColumn<TripDTO, Double> columnTripPrice;
    @FXML
    TableColumn<TripDTO, Integer> columnTripAvailableSeats;

    @FXML
    TableView<Reservation> tableReservations;
    @FXML
    TableColumn<Reservation, String> columnResClient;
    @FXML
    TableColumn<Reservation, String> columnResTrip;
    @FXML
    TableColumn<Reservation, String> columnResSeats;

    @FXML
    ComboBox<String> comboBoxTripLocation;
    @FXML
    Button buttonSearch;
    @FXML
    TextField textFieldAfter;
    @FXML
    TextField textFieldBefore;

    @FXML
    TextField textFieldClient;
    @FXML
    TextField textFieldPhoneNumber;
    @FXML
    TextField textFieldSeats;
    @FXML
    Button buttonReserve;
    @FXML
    Button buttonLogout;

    private String currentTripLocation;
    private String tripAfterTime;
    private String tripBeforeTime;

    public void initialize(Service srv, Agency agency) {
        this.srv = srv;
        this.loggedAgency = agency;
        this.agencyLabel.setText(agency.toString());
        reloadAgenciesList();
        currentTripLocation = "";
        initializeTripsTable();
        reloadTripsTable();
        initializeReservationsTable();
        reloadReservationsTable();
        reloadLocationComboBox();
    }

    private void initializeReservationsTable() {
        columnResClient.setCellValueFactory(new PropertyValueFactory<>("client"));
        columnResTrip.setCellValueFactory(c -> new SimpleStringProperty(
                c.getValue().getTrip().getTouristAttraction() + " at " +
                        c.getValue().getTrip().getDepartureTime()));
        columnResSeats.setCellValueFactory(new PropertyValueFactory<>("seats"));
    }

    private void reloadReservationsTable() {
        tableReservations.getItems().setAll(srv.getAllReservations());
    }

    private void initializeTripsTable() {
        tableTrips.setRowFactory(tv -> new TableRow<>() {
            @Override
            protected void updateItem(TripDTO item, boolean empty) {
                super.updateItem(item, empty);
                if (item == null)
                    setStyle("");
                else if (item.getAvailableSeats() <= 0)
                    setStyle("-fx-background-color: red;");
                else
                    setStyle("");
            }
        });

        columnTripAttraction.setCellValueFactory(new PropertyValueFactory<>("touristAttraction"));
        columnTripCompany.setCellValueFactory(new PropertyValueFactory<>("transportCompany"));
        columnTripDepartureTime.setCellValueFactory(new PropertyValueFactory<>("departureTime"));
        columnTripPrice.setCellValueFactory(new PropertyValueFactory<>("price"));
        columnTripAvailableSeats.setCellValueFactory(new PropertyValueFactory<>("availableSeats"));
    }

    private void reloadTripsTable() {
        if (currentTripLocation.isBlank())
            tableTrips.getItems().setAll(srv.getTripDTOs(srv.getAllTrips()));
        else {
            Time timeAfter;
            Time timeBefore;
            try {
                timeAfter = Time.valueOf(tripAfterTime);
                timeBefore = Time.valueOf(tripBeforeTime);
            } catch (IllegalArgumentException ex) {
                timeAfter = Time.valueOf("00:00:00");
                timeBefore = Time.valueOf("23:59:59");
            }
            tableTrips.getItems().setAll(srv.getTripDTOs(srv.getTouristAttractionTrips(
                    currentTripLocation, timeAfter, timeBefore)));
        }
    }

    private void reloadLocationComboBox() {
        Set<String> locations = new HashSet<>();
        srv.getAllTrips()
                .forEach(t -> locations.add(t.getTouristAttraction()));
        comboBoxTripLocation.getItems().setAll(locations);
    }

    private void reloadAgenciesList() {
        listAgencies.getItems().setAll(srv.getAllAgencies());
    }

    @FXML
    private void onChangeLocation() {
        currentTripLocation = comboBoxTripLocation.getValue();
        reloadTripsTable();
    }

    @FXML
    private void onButtonSearchClick() {
        tripAfterTime = textFieldAfter.getText();
        tripBeforeTime = textFieldBefore.getText();
        reloadTripsTable();
    }

    @FXML
    private void onButtonReserveClick() {
        if (tableTrips.getSelectionModel().getSelectedItems().isEmpty())
            return;

        String client = textFieldClient.getText();
        Trip trip = srv.getTrip(tableTrips.getSelectionModel().getSelectedItem().getId());
        int seats = Integer.parseInt(textFieldSeats.getText());
        String phoneNumber = textFieldPhoneNumber.getText();
        if (tableTrips.getSelectionModel().getSelectedItem().getAvailableSeats() < seats) {
            MyAlert.StartAlert("Error", "Not enough seats left!", Alert.AlertType.ERROR);
            return;
        }
        srv.saveReservation(client, trip, phoneNumber, seats);
        reloadReservationsTable();
    }

    @FXML
    private void onButtonLogoutClick() throws IOException {
        FXMLLoader loader = new FXMLLoader(MainSceneController.class.getResource("loginScene.fxml"));
        Parent root = loader.load();
        LoginSceneController controller = loader.getController();
        controller.initialize(this.srv);
        Stage stage = (Stage)anchorPane.getScene().getWindow();
        stage.getScene().setRoot(root);
    }
}
