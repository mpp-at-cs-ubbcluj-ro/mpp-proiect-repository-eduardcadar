package services;

public class ServiceException extends Exception {
    public ServiceException() {}
    public ServiceException(String msg) { super(msg); }
    public ServiceException(String msg, Throwable t) { super(msg, t); }
}
