package com.borsvy.service;

import com.borsvy.model.Stock;
import com.borsvy.repository.StockRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.asynchttpclient.*;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.util.List;
import java.util.Map;
import java.util.concurrent.CompletableFuture;

@Service
public class StockService {
    private static final Logger logger = LoggerFactory.getLogger(StockService.class);
    
    @Value("${finnhub.api.key}")
    private String apiKey;
    
    @Value("${finnhub.api.url}")
    private String apiUrl;
    
    @Autowired
    private StockRepository stockRepository;
    
    private final AsyncHttpClient client;
    private final ObjectMapper objectMapper;

    public StockService() {
        this.client = Dsl.asyncHttpClient();
        this.objectMapper = new ObjectMapper();
    }

    private CompletableFuture<Response> fetchQuote(String symbol) {
        String url = apiUrl + "/quote?symbol=" + symbol + "&token=" + apiKey;
        return client.prepareGet(url).execute().toCompletableFuture();
    }

    private CompletableFuture<Response> fetchCompanyProfile(String symbol) {
        String url = apiUrl + "/stock/profile2?symbol=" + symbol + "&token=" + apiKey;
        return client.prepareGet(url).execute().toCompletableFuture();
    }

    private CompletableFuture<Response> fetchPeers(String symbol) {
        String url = apiUrl + "/stock/peers?symbol=" + symbol + "&token=" + apiKey;
        return client.prepareGet(url).execute().toCompletableFuture();
    }

    private CompletableFuture<Response> fetchCompanySearch(String query) {
        String url = apiUrl + "/search?q=" + query + "&token=" + apiKey;
        logger.debug("Fetching company search from URL: {}", url);
        return client.prepareGet(url)
            .setRequestTimeout(5000) // 5 second timeout
            .execute()
            .toCompletableFuture();
    }

    public Stock getStockData(String symbol) {
        try {
            // Fetch quote, profile, and peers data in parallel
            CompletableFuture<Response> quoteFuture = fetchQuote(symbol);
            CompletableFuture<Response> profileFuture = fetchCompanyProfile(symbol);
            CompletableFuture<Response> peersFuture = fetchPeers(symbol);

            // Wait for all requests to complete
            CompletableFuture.allOf(quoteFuture, profileFuture, peersFuture).join();

            // Parse all data
            String quoteResponseBody = quoteFuture.get().getResponseBody();
            Map<String, Object> quoteData = objectMapper.readValue(quoteResponseBody, Map.class);

            String profileResponseBody = profileFuture.get().getResponseBody();
            Map<String, Object> profileData = objectMapper.readValue(profileResponseBody, Map.class);

            String peersResponseBody = peersFuture.get().getResponseBody();
            List<String> peersList = objectMapper.readValue(peersResponseBody, List.class);

            Stock stock = new Stock();
            stock.setSymbol(symbol);
            stock.setFavorite(stockRepository.existsById(symbol));

            // Set data from quote
            if (quoteData != null && !quoteData.isEmpty()) {
                Double currentPrice = (Double) quoteData.getOrDefault("c", 0.0);
                Double change = (Double) quoteData.getOrDefault("d", 0.0);
                Double previousClose = (Double) quoteData.getOrDefault("pc", 0.0);
                
                stock.setPrice(currentPrice);
                stock.setChange(change);
                
                // Calculate change percentage
                if (previousClose > 0) {
                    Double changePercent = (change / previousClose) * 100;
                    stock.setChangePercent(changePercent);
                } else {
                    stock.setChangePercent(0.0);
                }
            }

            // Set data from profile and build enhanced description
            if (profileData != null && !profileData.isEmpty()) {
                stock.setName((String) profileData.getOrDefault("name", symbol));
                String industry = (String) profileData.getOrDefault("finnhubIndustry", "N/A");
                stock.setIndustry(industry);
                stock.setWebsite((String) profileData.getOrDefault("weburl", "N/A"));
                
                // Set sector based on industry mapping
                String sector = mapIndustryToSector(industry);
                stock.setSector(sector);
                
                // Build enhanced description with proper market cap formatting
                StringBuilder descBuilder = new StringBuilder();
                String companyDesc = (String) profileData.getOrDefault("description", "");
                if (!companyDesc.isEmpty()) {
                    descBuilder.append(companyDesc);
                } else {
                    descBuilder.append(profileData.getOrDefault("name", "")).append(" ");
                    descBuilder.append("is a ").append(sector).append(" company in the ").append(industry).append(" industry. ");
                    
                    // Format market cap with US locale to ensure decimal point
                    Double marketCap = (Double) profileData.getOrDefault("marketCapitalization", 0.0);
                    descBuilder.append("Market Cap: $").append(String.format(java.util.Locale.US, "%.2fB", marketCap)).append(". ");
                    
                    descBuilder.append("IPO date: ").append(profileData.getOrDefault("ipo", "N/A")).append(". ");
                    descBuilder.append("Exchange: ").append(profileData.getOrDefault("exchange", "N/A"));
                }

                // Add peer companies if available
                if (peersList != null && !peersList.isEmpty()) {
                    descBuilder.append(". Similar companies: ");
                    descBuilder.append(String.join(", ", peersList.subList(0, Math.min(5, peersList.size()))));
                }

                stock.setDescription(descBuilder.toString());
            } else {
                setDefaultValues(stock, symbol);
            }

            return stock;
        } catch (Exception e) {
            logger.error("Error fetching stock data for symbol {}: {}", symbol, e.getMessage());
            throw new RuntimeException("Failed to fetch stock data for " + symbol + ": " + e.getMessage());
        }
    }

    private String mapIndustryToSector(String industry) {
        if (industry == null || industry.equals("N/A")) return "N/A";
        
        // Common industry to sector mappings
        if (industry.contains("Technology") || industry.contains("Software")) return "Technology";
        if (industry.contains("Bank") || industry.contains("Financial")) return "Financial Services";
        if (industry.contains("Healthcare") || industry.contains("Biotech")) return "Healthcare";
        if (industry.contains("Energy") || industry.contains("Oil")) return "Energy";
        if (industry.contains("Consumer") && industry.contains("Cyclical")) return "Consumer Cyclical";
        if (industry.contains("Consumer") && !industry.contains("Cyclical")) return "Consumer Defensive";
        if (industry.contains("Industrial")) return "Industrials";
        if (industry.contains("Material")) return "Basic Materials";
        if (industry.contains("Real Estate")) return "Real Estate";
        if (industry.contains("Telecom")) return "Communication Services";
        
        return industry; // Use industry as sector if no mapping found
    }

    private String formatMarketCap(Double marketCap) {
        if (marketCap == null || marketCap == 0.0) return "N/A";
        if (marketCap >= 1000) return String.format("%.2fB", marketCap);
        return String.format("%.2fM", marketCap * 1000);
    }

    private void setDefaultValues(Stock stock, String symbol) {
        stock.setName(symbol);
        stock.setDescription("Company information not available");
        stock.setSector("N/A");
        stock.setIndustry("N/A");
        stock.setWebsite("N/A");
    }

    public List<Stock> getFavorites() {
        return stockRepository.findAll();
    }

    public void addToFavorites(Stock stock) {
        try {
            if (stock == null || stock.getSymbol() == null || stock.getSymbol().isEmpty()) {
                throw new IllegalArgumentException("Invalid stock data: symbol is required");
            }

            // Get fresh stock data to ensure all fields are set
            Stock freshStock = getStockData(stock.getSymbol());
            freshStock.setFavorite(true); // Explicitly set favorite to true
            
            // Ensure all required fields are set
            if (freshStock.getName() == null) freshStock.setName(freshStock.getSymbol());
            if (freshStock.getDescription() == null) freshStock.setDescription("No description available");
            if (freshStock.getSector() == null) freshStock.setSector("N/A");
            if (freshStock.getIndustry() == null) freshStock.setIndustry("N/A");
            if (freshStock.getWebsite() == null) freshStock.setWebsite("N/A");
            
            // Log the stock data before saving
            logger.debug("Saving stock to favorites: symbol={}, name={}, favorite={}, price={}, change={}", 
                freshStock.getSymbol(), 
                freshStock.getName(), 
                freshStock.isFavorite(),
                freshStock.getPrice(),
                freshStock.getChange());
            
            stockRepository.save(freshStock);
            logger.info("Added stock to favorites: {}", freshStock.getSymbol());
        } catch (Exception e) {
            logger.error("Error adding stock to favorites: " + e.getMessage());
            throw new RuntimeException("Failed to add stock to favorites: " + e.getMessage());
        }
    }

    public void removeFromFavorites(String symbol) {
        stockRepository.deleteById(symbol);
    }

    public List<Map<String, String>> searchCompanies(String query) {
        try {
            Response response = fetchCompanySearch(query).join();
            String responseBody = response.getResponseBody();
            
            if (responseBody == null || responseBody.isEmpty()) {
                logger.warn("Empty response from Finnhub API for query: {}", query);
                return List.of();
            }
            
            Map<String, Object> searchData = objectMapper.readValue(responseBody, Map.class);
            
            // Check for API error response
            if (searchData.containsKey("error")) {
                logger.warn("Finnhub API error for query '{}': {}", query, searchData.get("error"));
                return List.of();
            }
            
            if (!searchData.containsKey("result")) {
                logger.warn("Invalid response format from Finnhub API for query: {}", query);
                return List.of();
            }
            
            @SuppressWarnings("unchecked")
            List<Map<String, Object>> results = (List<Map<String, Object>>) searchData.get("result");
            
            if (results == null || results.isEmpty()) {
                logger.info("No results found for query: {}", query);
                return List.of();
            }
            
            return results.stream()
                .filter(result -> result.get("symbol") != null && result.get("description") != null)
                .map(result -> Map.of(
                    "symbol", (String) result.get("symbol"),
                    "name", (String) result.get("description")
                ))
                .toList();
        } catch (Exception e) {
            logger.error("Error searching companies for query '{}': {}", query, e.getMessage());
            return List.of(); // Return empty list instead of throwing
        }
    }

    public void destroy() {
        try {
            client.close();
        } catch (Exception e) {
            logger.error("Error closing HTTP client: {}", e.getMessage());
        }
    }
}
