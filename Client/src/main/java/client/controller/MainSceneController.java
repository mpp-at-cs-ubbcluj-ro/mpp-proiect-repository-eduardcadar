package client.controller;

import client.dtos.TripDTO;
import client.utils.MyAlert;
import javafx.application.Platform;
import javafx.beans.property.SimpleStringProperty;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import model.domain.Agency;
import model.domain.Reservation;
import model.domain.Trip;
import model.utils.Pair;
import services.IObserver;
import services.IServices;
import services.ServiceException;

import java.sql.Time;
import java.util.*;


public class MainSceneController implements IObserver {
    private IServices srv;
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
    TableColumn<Reservation, String> columnResAgency;
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
    private final Time minTime = Time.valueOf("00:00:00");
    private final Time maxTime = Time.valueOf("23:59:59");

    public void setServer(IServices srv) {
        this.srv = srv;
    }

    public void setAgency(Agency agency) {
        this.loggedAgency = agency;
        this.agencyLabel.setText(agency.toString());
    }

    public void initializeTables() {
        reloadAgenciesList();
        initializeReservationsTable();
        reloadReservationsTable();
        currentTripLocation = "";
        initializeTripsTable();
        reloadLocationComboBox();
    }

    private void reloadAgenciesList() {
        Platform.runLater(() -> {
            try {
                listAgencies.getItems().setAll(srv.getAgencies());
            } catch (ServiceException e) {
                MyAlert.StartAlert("Reload agencies error", e.getMessage(), Alert.AlertType.ERROR);
            }
        });
    }

    private void initializeReservationsTable() {
        columnResClient.setCellValueFactory(new PropertyValueFactory<>("client"));
        columnResTrip.setCellValueFactory(c -> new SimpleStringProperty(
                c.getValue().getTrip().getTouristAttraction() + " at " +
                        c.getValue().getTrip().getDepartureTime()));
        columnResAgency.setCellValueFactory(new PropertyValueFactory<>("agency"));
        columnResSeats.setCellValueFactory(new PropertyValueFactory<>("seats"));
    }

    private void reloadReservationsTable() {
        Platform.runLater(() -> {
            try {
                tableReservations.getItems().setAll(srv.getReservations());
            } catch (ServiceException ex) {
                MyAlert.StartAlert("Get reservations error", ex.getMessage(), Alert.AlertType.ERROR);
            }
        });
    }

    private void initializeTripsTable() {
        tableTrips.setRowFactory(tv -> new TableRow<>() {
            @Override
            protected void updateItem(TripDTO item, boolean empty) {
                super.updateItem(item, empty);
                if (item == null) setStyle("");
                else if (item.getAvailableSeats() <= 0) setStyle("-fx-background-color: red;");
                else setStyle("");
            }
        });

        columnTripAttraction.setCellValueFactory(new PropertyValueFactory<>("touristAttraction"));
        columnTripCompany.setCellValueFactory(new PropertyValueFactory<>("transportCompany"));
        columnTripDepartureTime.setCellValueFactory(new PropertyValueFactory<>("departureTime"));
        columnTripPrice.setCellValueFactory(new PropertyValueFactory<>("price"));
        columnTripAvailableSeats.setCellValueFactory(new PropertyValueFactory<>("availableSeats"));

        try {
            Collection<Trip> trips = srv.getTrips("", minTime, maxTime);
            Collection<TripDTO> tripDTOS = getTripDTOs(trips, srv.getReservations());
            Platform.runLater(() -> tableTrips.getItems().setAll(tripDTOS));
        } catch (ServiceException e) {
            MyAlert.StartAlert("Error", e.getMessage(), Alert.AlertType.ERROR);
        }
    }

    private void reloadTripsTable() {
        Time timeAfter;
        Time timeBefore;
        try {
            timeAfter = Time.valueOf(tripAfterTime);
        } catch (IllegalArgumentException ex) {
            timeAfter = minTime;
        }
        try {
            timeBefore = Time.valueOf(tripBeforeTime);
        } catch (IllegalArgumentException ex) {
            timeBefore = maxTime;
        }
        try {
            Collection<Trip> trips = srv.getTrips(
                    currentTripLocation, timeAfter, timeBefore);

            Collection<Reservation> reservations = tableReservations.getItems().stream().toList();
            Collection<TripDTO> tripDTOS = getTripDTOs(trips, reservations);
            Platform.runLater(() -> tableTrips.getItems().setAll(tripDTOS));
        } catch (ServiceException ex) {
            MyAlert.StartAlert("Get trips error", ex.getMessage(), Alert.AlertType.ERROR);
        }
    }

    private void reloadLocationComboBox() {
        Set<String> locations = new HashSet<>();
        try {
            srv.getTrips("", minTime, maxTime)
                    .forEach(t -> locations.add(t.getTouristAttraction()));
        } catch (ServiceException ex) {
            MyAlert.StartAlert("Get trips error", ex.getMessage(), Alert.AlertType.ERROR);
            return;
        }
        Platform.runLater(() -> comboBoxTripLocation.getItems().setAll(locations));
    }

    @FXML
    private void onButtonSearchClick() {
        if (!comboBoxTripLocation.getSelectionModel().isEmpty())
            currentTripLocation = comboBoxTripLocation.getValue();
        tripAfterTime = textFieldAfter.getText();
        tripBeforeTime = textFieldBefore.getText();
        reloadTripsTable();
    }

    @FXML
    private void onButtonReserveClick() {
        if (tableTrips.getSelectionModel().getSelectedItems().isEmpty()) {
            MyAlert.StartAlert("No trip selected", "Select a trip", Alert.AlertType.WARNING);
            return;
        }

        String client = textFieldClient.getText();
        Trip trip = tableTrips.getSelectionModel().getSelectedItem().getTrip();
        int seats;
        try {
            seats = Integer.parseInt(textFieldSeats.getText());
            if (seats <= 0) throw new NumberFormatException();
        } catch (NumberFormatException e) {
            MyAlert.StartAlert("Error", "Invalid seats number", Alert.AlertType.ERROR);
            return;
        }
        String phoneNumber = textFieldPhoneNumber.getText();
        if (tableTrips.getSelectionModel().getSelectedItem().getAvailableSeats() < seats) {
            MyAlert.StartAlert("Error", "Not enough seats left!", Alert.AlertType.ERROR);
            return;
        }
        Reservation reservation = new Reservation(new Pair<>(client, trip), loggedAgency.getName(), phoneNumber, seats);
        try {
            srv.saveReservation(reservation);
        } catch (ServiceException e) {
            MyAlert.StartAlert("Save reservation error", e.getMessage(), Alert.AlertType.ERROR);
            return;
        }
        Platform.runLater(() -> tableReservations.getItems().add(reservation));
        tableTrips.getItems().stream()
                .filter(t -> t.getId() == reservation.getTrip().getId())
                .forEach(t -> t.setAvailableSeats(t.getAvailableSeats() - reservation.getSeats()));
        Platform.runLater(() -> tableTrips.refresh());
    }

    @FXML
    public void onButtonLogoutClick() {
        logout();
        System.exit(0);
    }

    @Override
    public void ReservationSaved(Reservation reservation) {
        Platform.runLater(() -> tableReservations.getItems().add(reservation));
        tableTrips.getItems().stream()
                .filter(t -> t.getId() == reservation.getTrip().getId())
                .forEach(t -> t.setAvailableSeats(t.getAvailableSeats() - reservation.getSeats()));
        Platform.runLater(() -> tableTrips.refresh());
    }

    private Collection<TripDTO> getTripDTOs(Collection<Trip> trips, Collection<Reservation> reservations) {
        List<TripDTO> tripDTOs = new ArrayList<>();
        if (trips != null && !trips.isEmpty())
            trips.forEach(t -> {
                int reservedSeats;
                reservedSeats = reservations.stream()
                        .filter(r -> r.getTrip().getId().equals(t.getId()))
                        .mapToInt(Reservation::getSeats)
                        .sum();
                int availableSeats = t.getSeats() - reservedSeats;

                TripDTO tripDTO = new TripDTO(t.getId(), t, t.getTouristAttraction(), t.getTransportCompany(),
                        t.getDepartureTime(), t.getPrice(), availableSeats);
                tripDTOs.add(tripDTO);
            });
        return tripDTOs;
    }

    public void logout() {
        try {
            srv.logout(loggedAgency, this);
        } catch (ServiceException ex) {
            System.out.println("Logout error: " + ex);
        }
    }
}
