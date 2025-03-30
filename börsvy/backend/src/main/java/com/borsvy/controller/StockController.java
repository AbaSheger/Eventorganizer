package com.borsvy.controller;

import com.borsvy.model.StockPrice;
import com.borsvy.model.StockDetails;
import com.borsvy.model.StockAnalysis;
import com.borsvy.model.Stock;
import com.borsvy.service.StockService;
import com.borsvy.service.AnalysisService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.http.HttpStatus;
import java.util.List;
import java.util.Optional;
import java.util.concurrent.ConcurrentHashMap;
import java.time.Instant;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@RestController
@RequestMapping("/api/stocks")
public class StockController {

    private static final Logger log = LoggerFactory.getLogger(StockController.class);

    @Autowired
    private StockService stockService;
    
    @Autowired
    private AnalysisService analysisService;

    private final ConcurrentHashMap<String, Long> lastSearchTime = new ConcurrentHashMap<>();
    private static final long SEARCH_RATE_LIMIT_MS = 1000; // 1 second between searches per IP
    private static final long CACHE_CLEANUP_INTERVAL_MS = 5 * 60 * 1000; // 5 minutes
    private final ScheduledExecutorService scheduler = Executors.newSingleThreadScheduledExecutor();

    public StockController() {
        
        // Schedule cache cleanup
        scheduler.scheduleAtFixedRate(
            this::cleanupRateLimitCache,
            CACHE_CLEANUP_INTERVAL_MS,
            CACHE_CLEANUP_INTERVAL_MS,
            TimeUnit.MILLISECONDS
        );
    }

    private void cleanupRateLimitCache() {
        long now = Instant.now().toEpochMilli();
        lastSearchTime.entrySet().removeIf(entry -> 
            now - entry.getValue() > SEARCH_RATE_LIMIT_MS * 2
        );
    }

    @GetMapping("/popular")
    public List<Stock> getPopularStocks() {
        return stockService.getPopularStocks();
    }

    @GetMapping("/{symbol}/history")
    public ResponseEntity<List<StockPrice>> getPriceHistory(
            @PathVariable String symbol,
            @RequestParam(defaultValue = "1D") String interval) {
        try {
            List<StockPrice> priceHistory = stockService.getPriceHistory(symbol, interval);
            return ResponseEntity.ok(priceHistory);
        } catch (Exception e) {
            e.printStackTrace();
            // In case of any error, return an empty list instead of 500 error
            return ResponseEntity.ok(stockService.generateDummyPriceHistory(symbol, interval));
        }
    }

    @GetMapping("/{symbol}/details")
    public ResponseEntity<StockDetails> getStockDetails(@PathVariable String symbol) {
        try {
            StockDetails details = stockService.getStockDetails(symbol);
            return ResponseEntity.ok(details);
        } catch (Exception e) {
            // Return dummy data instead of error
            try {
                return ResponseEntity.ok(stockService.getStockDetails(symbol));
            } catch (Exception ex) {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
            }
        }
    }

    @GetMapping("/{symbol}/analysis")
    public ResponseEntity<Map<String, Object>> getAnalysis(@PathVariable String symbol) {
        try {
            Map<String, Object> analysis = analysisService.getCompleteAnalysis(symbol);
            if (analysis != null) {
                log.debug("Analysis response for {}: {}", symbol, analysis);
                if (analysis.containsKey("recentNews")) {
                    log.debug("News articles in response: {}", analysis.get("recentNews"));
                } else {
                    log.warn("No news articles found in analysis response for {}", symbol);
                }
                return ResponseEntity.ok(analysis);
            }
            return ResponseEntity.notFound().build();
        } catch (Exception e) {
            log.error("Error getting analysis for {}: {}", symbol, e.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
        }
    }

    @GetMapping("/search")
    public ResponseEntity<?> searchStocks(@RequestParam String query, @RequestHeader(value = "X-Forwarded-For", required = false) String clientIp) {
        if (query == null || query.trim().isEmpty()) {
            return ResponseEntity.badRequest().body("Search query cannot be empty");
        }

        // Use client IP or a default key if not available
        String key = clientIp != null ? clientIp : "default";
        
        // Check rate limit
        Long lastSearch = lastSearchTime.get(key);
        if (lastSearch != null && Instant.now().toEpochMilli() - lastSearch < SEARCH_RATE_LIMIT_MS) {
            return ResponseEntity
                .status(HttpStatus.TOO_MANY_REQUESTS)
                .body("Please wait before making another search request");
        }
        
        // Update last search time
        lastSearchTime.put(key, Instant.now().toEpochMilli());
        
        try {
            // Perform search
            List<Stock> results = stockService.searchStocks(query);
            return ResponseEntity.ok(results);
        } catch (Exception e) {
            return ResponseEntity
                .status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body("An error occurred while searching for stocks");
        }
    }

    @GetMapping("/{symbol}/chart")
    public ResponseEntity<List<Map<String, Object>>> getChartData(
            @PathVariable String symbol,
            @RequestParam(defaultValue = "1D") String interval) {
        try {
            List<Map<String, Object>> chartData = stockService.getChartData(symbol, interval);
            return ResponseEntity.ok(chartData);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
        }
    }

    @GetMapping("/{symbol}/peers")
    public ResponseEntity<Map<String, Object>> getPeerComparison(@PathVariable String symbol) {
        try {
            Map<String, Object> peers = stockService.getPeerComparison(symbol);
            return ResponseEntity.ok(peers);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
        }
    }
}