package model.domain;

import java.io.Serializable;

public interface Identifiable<Tid> extends Serializable {
    Tid getId();
    void setId(Tid id);
}
