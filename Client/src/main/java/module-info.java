module client {
    requires javafx.controls;
    requires javafx.fxml;
    requires model;
    requires services;
    requires java.sql;
    requires rpc;

    opens client.controller to javafx.fxml, javafx.base;
    opens client.dtos to javafx.fxml, javafx.base;
    opens client.utils to javafx.fxml, javafx.base;
    exports client;
}