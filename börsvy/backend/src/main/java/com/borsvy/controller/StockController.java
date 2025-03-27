package com.borsvy.controller;

import com.borsvy.model.Stock;
import com.borsvy.service.StockService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/stocks")
@CrossOrigin(origins = {"http://localhost:3000", "http://localhost:3001", "http://localhost:3005"})
public class StockController {
    
    private static final Logger logger = LoggerFactory.getLogger(StockController.class);

    @Autowired
    private StockService stockService;

    @GetMapping("/search/symbol/{symbol}")
    public ResponseEntity<?> getStockDataBySymbol(@PathVariable String symbol) {
        try {
            Stock stock = stockService.getStockData(symbol);
            return ResponseEntity.ok(stock);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                .body(Map.of("error", "Stock not found: " + e.getMessage()));
        }
    }

    @GetMapping("/search/company/{query}")
    public ResponseEntity<List<Map<String, String>>> searchCompanies(@PathVariable String query) {
        try {
            List<Map<String, String>> companies = stockService.searchCompanies(query);
            return ResponseEntity.ok(companies);
        } catch (Exception e) {
            logger.error("Error searching companies for query '{}': {}", query, e.getMessage());
            return ResponseEntity.ok(List.of());
        }
    }

    @GetMapping("/favorites")
    public List<Stock> getFavorites() {
        return stockService.getFavorites();
    }

    @PostMapping("/favorites")
    public ResponseEntity<?> addToFavorites(@RequestBody Stock stock) {
        try {
            stockService.addToFavorites(stock);
            return ResponseEntity.ok().build();
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                .body(Map.of("error", "Failed to add stock to favorites: " + e.getMessage()));
        }
    }

    @DeleteMapping("/favorites/{symbol}")
    public ResponseEntity<?> removeFromFavorites(@PathVariable String symbol) {
        try {
            stockService.removeFromFavorites(symbol);
            return ResponseEntity.ok().build();
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                .body(Map.of("error", "Failed to remove stock from favorites: " + e.getMessage()));
        }
    }
}
