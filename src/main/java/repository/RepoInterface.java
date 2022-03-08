package repository;

import java.util.Collection;

public interface RepoInterface<T, Tid> {
    void save(T elem);
    void delete(T elem);
    void deleteById(Tid id);
    void update(T elem, Tid id);
    T getById(Tid id);
    void clear();
    Collection<T> getAll();
}
