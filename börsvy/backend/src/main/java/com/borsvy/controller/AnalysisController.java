package com.borsvy.controller;

import com.borsvy.model.StockAnalysis;
import com.borsvy.service.AnalysisService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@RestController
@RequestMapping("/api/analysis")
public class AnalysisController {

    @Autowired
    private AnalysisService analysisService;
    
    @GetMapping("/{symbol}")
    public ResponseEntity<StockAnalysis> getAnalysis(@PathVariable String symbol) {
        Optional<StockAnalysis> analysis = analysisService.getAnalysisForStock(symbol);
        
        if (analysis.isEmpty()) {
            return ResponseEntity.notFound().build();
        }
        
        return ResponseEntity.ok(analysis.get());
    }
}