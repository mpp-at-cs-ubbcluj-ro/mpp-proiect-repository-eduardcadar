package client;

import client.controller.LoginSceneController;
import client.controller.MainSceneController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import rpc.ServicesRpcProxy;
import services.IServices;

import java.io.IOException;
import java.util.Properties;

public class StartClient extends Application {
    private Stage primaryStage;
    private static final int defaultPort = 55553;
    private static final String defaultServer = "localhost";

    public void start(Stage primaryStage) throws Exception {
        System.out.println("Starting..");
        Properties clientProps = new Properties();
        try {
            clientProps.load(StartClient.class.getResourceAsStream("/client.properties"));
            System.out.println("Client properties set");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Properties error: " + e);
            return;
        }
        String serverIP = clientProps.getProperty("server.host", defaultServer);
        int serverPort = defaultPort;

        try {
            serverPort = Integer.parseInt(clientProps.getProperty("server.port"));
        } catch (NumberFormatException e) {
            System.err.println("Wrong port number " + e.getMessage());
            System.out.println("Using default port: " + defaultPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        IServices server = new ServicesRpcProxy(serverIP, serverPort);

        FXMLLoader loginLoader = new FXMLLoader(getClass().getClassLoader().getResource("controller/loginScene.fxml"));
        Parent loginRoot = loginLoader.load();
        LoginSceneController loginCtrl = loginLoader.getController();
        loginCtrl.setServer(server);

        FXMLLoader mainLoader = new FXMLLoader(getClass().getClassLoader().getResource("controller/mainScene.fxml"));
        Parent mainRoot = mainLoader.load();
        MainSceneController mainCtrl = mainLoader.getController();
        mainCtrl.setServer(server);

        loginCtrl.setMainController(mainCtrl);
        loginCtrl.setParent(mainRoot);

        primaryStage.setTitle("Agentii de turism");
        Scene loginScene = new Scene(loginRoot);
        primaryStage.setScene(loginScene);
        primaryStage.show();
    }
}
