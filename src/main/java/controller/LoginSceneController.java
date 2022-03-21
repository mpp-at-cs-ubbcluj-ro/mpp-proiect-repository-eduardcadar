package controller;

import domain.Agency;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import repository.MyException;
import service.Service;
import utils.MyAlert;

import java.io.IOException;

public class LoginSceneController {
    private Service srv;

    @FXML
    AnchorPane anchorPane;
    @FXML
    TextField textFieldUsername;
    @FXML
    TextField textFieldPassword;
    @FXML
    Button buttonLogin;

    public void initialize(Service service) {
        this.srv = service;
    }

    private Agency login() {
        String agencyName = textFieldUsername.getText();
        String password = textFieldPassword.getText();
        Agency agency = srv.getAgencyByName(agencyName);
        if (agency.getPassword().equals(password))
            return agency;
        throw new MyException("Wrong password!");
    }

    @FXML
    public void onButtonLoginClick() throws IOException {
        Agency agency;
        try {
            agency = login();
        } catch (MyException ex) {
            MyAlert.StartAlert("Login error", ex.getMsg(), Alert.AlertType.ERROR);
            return;
        }

        FXMLLoader loader = new FXMLLoader(LoginSceneController.class.getResource("mainScene.fxml"));
        Parent root = loader.load();
        MainSceneController controller = loader.getController();
        controller.initialize(this.srv, agency);
        Stage stage = (Stage)anchorPane.getScene().getWindow();
        stage.getScene().setRoot(root);
    }
}
