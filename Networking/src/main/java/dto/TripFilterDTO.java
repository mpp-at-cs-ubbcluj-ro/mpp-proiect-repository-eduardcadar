package dto;

import java.io.Serializable;
import java.sql.Time;

public record TripFilterDTO(String destination, Time startTime, Time endTime) implements Serializable { }
