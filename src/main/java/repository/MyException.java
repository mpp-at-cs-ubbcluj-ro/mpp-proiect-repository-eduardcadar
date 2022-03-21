package repository;

public class MyException extends RuntimeException {
    private final String errorMsg;
    public MyException(String message) { this.errorMsg = message; }
    public String getMsg() { return this.errorMsg; }
}
