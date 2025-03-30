package com.borsvy.service;

import com.borsvy.model.Stock;
import com.borsvy.model.StockAnalysis;
import com.borsvy.repository.StockAnalysisRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import lombok.extern.slf4j.Slf4j;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@Slf4j
@Service
public class AnalysisService {

    private final StockService stockService;
    private final StockAnalysisRepository analysisRepository;
    
    @Autowired
    public AnalysisService(StockService stockService, StockAnalysisRepository analysisRepository) {
        this.stockService = stockService;
        this.analysisRepository = analysisRepository;
    }
    
    public Map<String, Object> getCompleteAnalysis(String symbol) {
        Map<String, Object> completeAnalysis = new HashMap<>();
        
        try {
            // Get basic stock data
            Optional<Stock> stockOpt = stockService.getStockBySymbol(symbol);
            if (stockOpt.isEmpty()) {
                completeAnalysis.put("error", "Stock not found");
                return completeAnalysis;
            }
            
            Stock stock = stockOpt.get();
            completeAnalysis.put("stock", stock);
            
            // Add technical and fundamental analysis
            Map<String, Object> analysis = stockService.getAnalysis(symbol);
            completeAnalysis.put("fundamentalAnalysis", analysis.get("fundamental"));
            completeAnalysis.put("technicalAnalysis", analysis.get("technical"));
            completeAnalysis.put("sentiment", analysis.get("sentiment"));
            completeAnalysis.put("recommendation", analysis.get("recommendation"));
            
            // Add peer comparison data
            Map<String, Object> peerComparison = stockService.getPeerComparison(symbol);
            completeAnalysis.put("peerComparison", peerComparison);
            
            // Add news sentiment analysis
            Map<String, Object> newsSentiment = stockService.getNewsSentiment(symbol);
            completeAnalysis.put("newsSentiment", newsSentiment);
            
            // Get recent news articles
            List<Map<String, Object>> newsArticles = stockService.getStockNews(symbol, 5);
            log.info("Fetched {} news articles for {}", newsArticles.size(), symbol);
            log.debug("News articles: {}", newsArticles);
            completeAnalysis.put("recentNews", newsArticles);
            
            // Save analysis to database for later reference
            saveAnalysisToDatabase(symbol, analysis, newsSentiment);
            
            log.debug("Complete analysis response: {}", completeAnalysis);
            return completeAnalysis;
        } catch (Exception e) {
            log.error("Error generating complete analysis for {}: {}", symbol, e.getMessage());
            completeAnalysis.put("error", "Failed to generate analysis: " + e.getMessage());
            return completeAnalysis;
        }
    }
    
    private void saveAnalysisToDatabase(String symbol, Map<String, Object> analysis, Map<String, Object> newsSentiment) {
        try {
            StockAnalysis stockAnalysis = new StockAnalysis();
            stockAnalysis.setSymbol(symbol);
            stockAnalysis.setTimestamp(LocalDateTime.now());
            stockAnalysis.setRecommendation((String) analysis.get("recommendation"));
            stockAnalysis.setSentiment((String) analysis.get("sentiment"));
            
            // Add news sentiment if available
            if (newsSentiment != null && newsSentiment.containsKey("sentiment")) {
                stockAnalysis.setNewsSentiment((String) newsSentiment.get("sentiment"));
            }
            
            analysisRepository.save(stockAnalysis);
        } catch (Exception e) {
            log.error("Error saving analysis to database: {}", e.getMessage());
        }
    }
    
    public Map<String, Object> getHistoricalAnalysis(String symbol) {
        Map<String, Object> result = new HashMap<>();
        
        try {
            List<StockAnalysis> analyses = analysisRepository.findBySymbolOrderByTimestampDesc(symbol);
            result.put("analyses", analyses);
            return result;
        } catch (Exception e) {
            log.error("Error fetching historical analyses: {}", e.getMessage());
            result.put("error", "Failed to fetch historical analyses");
            return result;
        }
    }
}
