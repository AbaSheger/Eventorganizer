package com.borsvy.model;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Column;
import jakarta.persistence.Lob;
import jakarta.persistence.Table;

@Entity
@Table(name = "stock")
public class Stock {
    @Id
    private String symbol;
    
    private String name;
    private double price;
    private double change;
    private double changePercent;
    
    @Column(name = "favorite", nullable = false)
    private boolean favorite = false;
    
    @Lob
    @Column(length = 2000)
    private String description;
    private String sector;
    private String industry;
    private String website;
    
    // Default constructor
    public Stock() {
        this.favorite = false;
    }

    // Getters and Setters
    public String getSymbol() { return symbol; }
    public void setSymbol(String symbol) { this.symbol = symbol; }
    
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    
    public double getPrice() { return price; }
    public void setPrice(double price) { this.price = price; }
    
    public double getChange() { return change; }
    public void setChange(double change) { this.change = change; }
    
    public double getChangePercent() { return changePercent; }
    public void setChangePercent(double changePercent) { this.changePercent = changePercent; }
    
    public boolean isFavorite() { return favorite; }
    public void setFavorite(boolean favorite) { this.favorite = favorite; }

    // New getters and setters for profile information
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    
    public String getSector() { return sector; }
    public void setSector(String sector) { this.sector = sector; }
    
    public String getIndustry() { return industry; }
    public void setIndustry(String industry) { this.industry = industry; }
    
    public String getWebsite() { return website; }
    public void setWebsite(String website) { this.website = website; }
}
