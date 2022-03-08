package domain;

public class Agency implements Identifiable<Integer> {
    private int id;
    private final String name;
    private String password;

    public Agency(String name, String password) {
        this.name = name;
        this.password = password;
    }

    public Agency(int id, String name, String password) {
        this.id = id;
        this.name = name;
        this.password = password;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public String getPassword() {
        return password;
    }
}
