package repository;

import model.utils.MyException;

import java.util.Collection;

public interface IRepo<T, Tid> {
    void save(T elem) throws MyException;
    T getById(Tid id);
    Collection<T> getAll();
}
