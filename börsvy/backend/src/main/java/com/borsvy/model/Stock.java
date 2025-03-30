package com.borsvy.model;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.Data;
import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "stocks")
public class Stock {
    @Id
    private String symbol;
    private String name;
    private String industry;
    private double price;
    private double change;
    private double changePercent;
    private double high;
    private double low;
    private double open;
    private long volume;
    private double marketCap;
    private double peRatio;
    private double beta;
    private double high52Week;
    private double low52Week;
    private LocalDateTime lastUpdated;
}