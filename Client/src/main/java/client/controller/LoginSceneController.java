package client.controller;

import javafx.event.ActionEvent;
import javafx.scene.Node;
import model.domain.Agency;
import javafx.fxml.FXML;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import services.IServices;
import services.ServiceException;
import client.utils.MyAlert;

public class LoginSceneController {
    private IServices srv;
    private MainSceneController mainCtrl;
    private Agency crtAgency;
    Parent mainParent;

    @FXML
    AnchorPane anchorPane;
    @FXML
    TextField textFieldUsername;
    @FXML
    TextField textFieldPassword;
    @FXML
    Button buttonLogin;

    public void setMainController(MainSceneController mainCtrl) {
        this.mainCtrl = mainCtrl;
    }

    public void setParent(Parent parent) {
        this.mainParent = parent;
    }

    public void setServer(IServices service) {
        this.srv = service;
    }

    @FXML
    public void onButtonLoginClick(ActionEvent actionEvent) {
        String name = textFieldUsername.getText();
        String password = textFieldPassword.getText();
        crtAgency = new Agency(name, password);
        try {
            srv.login(crtAgency, mainCtrl);

            Stage stage = new Stage();
            stage.setTitle("Agentii de turism");
            stage.setScene(new Scene(mainParent));
            stage.setOnCloseRequest(event -> {
                mainCtrl.logout();
                System.exit(0);
            });

            stage.show();
            mainCtrl.setAgency(crtAgency);
            mainCtrl.initializeTables();
            ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
        } catch (ServiceException ex) {
            MyAlert.StartAlert("Login error", ex.getMessage(), Alert.AlertType.ERROR);
        }
    }
}
