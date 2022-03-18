package controller;

import domain.Agency;
import domain.Reservation;
import domain.Trip;
import javafx.beans.property.SimpleStringProperty;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import service.Service;

import java.sql.Time;
import java.util.HashSet;
import java.util.Set;


public class MainSceneController {
    private Service srv;

    @FXML
    ListView<Agency> listAgencies;

    @FXML
    TableView<Trip> tableTrips;
    @FXML
    TableColumn<Trip, String> columnTripAttraction;
    @FXML
    TableColumn<Trip, String> columnTripCompany;
    @FXML
    TableColumn<Trip, Time> columnTripDepartureTime;
    @FXML
    TableColumn<Trip, Double> columnTripPrice;
    @FXML
    TableColumn<Trip, Integer> columnTripSeats;

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

    private String currentTripLocation;
    private String tripAfterTime;
    private String tripBeforeTime;

    public void initialize(Service srv) {
        this.srv = srv;
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
        columnTripAttraction.setCellValueFactory(new PropertyValueFactory<>("touristAttraction"));
        columnTripCompany.setCellValueFactory(new PropertyValueFactory<>("transportCompany"));
        columnTripDepartureTime.setCellValueFactory(new PropertyValueFactory<>("departureTime"));
        columnTripPrice.setCellValueFactory(new PropertyValueFactory<>("price"));
        columnTripSeats.setCellValueFactory(new PropertyValueFactory<>("seats"));
    }

    private void reloadTripsTable() {
        if (currentTripLocation.isBlank())
            tableTrips.getItems().setAll(srv.getAllTrips());
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
            tableTrips.getItems().setAll(srv.getTouristAttractionTrips(
                    currentTripLocation, timeAfter, timeBefore));
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
        String client = textFieldClient.getText();
        Trip trip = srv.getTrip(tableTrips.getSelectionModel().getSelectedItem().getId());
        int seats = Integer.parseInt(textFieldSeats.getText());
        String phoneNumber = textFieldPhoneNumber.getText();
        srv.saveReservation(client, trip, phoneNumber, seats);
        reloadReservationsTable();
    }
}
