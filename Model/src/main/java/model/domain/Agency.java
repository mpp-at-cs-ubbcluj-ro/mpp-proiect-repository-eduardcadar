package model.domain;

public class Agency implements Identifiable<String> {
    private String name;
    private String password;

    public Agency(String name, String password) {
        this.name = name;
        this.password = password;
    }

    @Override
    public String getId() {
        return name;
    }

    @Override
    public void setId(String id) {
        this.name = id;
    }

    public String getName() {
        return name;
    }

    public String getPassword() {
        return password;
    }

    @Override
    public String toString() {
        return "Agency: " + name;
    }
}
