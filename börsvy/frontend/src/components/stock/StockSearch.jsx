import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { stockService } from '../../services/api';
import { EXAMPLE_SYMBOLS, ERROR_MESSAGES } from '../../constants';
import '../../styles/StockSearch.css';

// Common stock search patterns
const COMMON_SEARCHES = {
    tech: ['AAPL', 'GOOGL', 'MSFT', 'META', 'NVDA', 'AMD', 'INTC', 'CRM', 'ADBE', 'PYPL'],
    finance: ['JPM', 'BAC', 'V', 'MA', 'GS', 'MS', 'BLK', 'AXP', 'WFC', 'SCHW'],
    healthcare: ['JNJ', 'PFE', 'MRK', 'ABBV', 'UNH', 'ABT', 'BMY', 'AMGN', 'GILD', 'MRNA'],
    retail: ['WMT', 'AMZN', 'TGT', 'HD', 'COST', 'LOW', 'MCD', 'SBUX', 'NKE', 'TJX'],
    energy: ['XOM', 'CVX', 'COP', 'SLB', 'EOG', 'MPC', 'PSX', 'VLO', 'OXY', 'PXD'],
    automotive: ['TSLA', 'F', 'GM', 'TM', 'HMC', 'STLA', 'RIVN', 'LCID', 'NIO', 'XPEV'],
    popular: ['AAPL', 'GOOGL', 'MSFT', 'AMZN', 'TSLA', 'NVDA', 'META', 'JPM', 'V', 'WMT']
};

function StockSearch({ onStockFound }) {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchResults, setSearchResults] = useState([]);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [searchTimeout, setSearchTimeout] = useState(null);
    const [showExamples, setShowExamples] = useState(true);

    useEffect(() => {
        return () => {
            if (searchTimeout) {
                clearTimeout(searchTimeout);
            }
        };
    }, [searchTimeout]);

    const handleSearch = async () => {
        if (!searchTerm.trim()) {
            setError('Please enter a stock symbol or company name');
            return;
        }

        try {
            setError(null);
            setIsLoading(true);
            const data = await stockService.searchStock(searchTerm);
            if (Array.isArray(data)) {
                setSearchResults(data);
            } else {
                onStockFound(data);
                setSearchTerm('');
                setSearchResults([]);
            }
        } catch (error) {
            setError(error instanceof Error ? error.message : ERROR_MESSAGES.STOCK_NOT_FOUND);
        } finally {
            setIsLoading(false);
        }
    };

    const handleCompanySearch = async (query) => {
        if (query.length < 2) {
            setSearchResults([]);
            return;
        }

        try {
            const companies = await stockService.searchCompanies(query);
            setSearchResults(companies);
            setError(null);
        } catch (error) {
            console.error('Company search error:', error);
            setSearchResults([]);
            setError(null);
        }
    };

    const handleInputChange = (e) => {
        const value = e.target.value;
        setSearchTerm(value);
        setError(null);
        setShowExamples(false);

        // Clear existing timeout
        if (searchTimeout) {
            clearTimeout(searchTimeout);
        }

        // Set new timeout for company search
        if (!/^[A-Z]+$/.test(value)) {
            const timeout = setTimeout(() => handleCompanySearch(value), 300);
            setSearchTimeout(timeout);
        } else {
            setSearchResults([]);
        }
    };

    const handleCompanySelect = async (symbol) => {
        setSearchTerm(symbol);
        setSearchResults([]);
        try {
            setIsLoading(true);
            const data = await stockService.searchStock(symbol);
            onStockFound(data);
            setSearchTerm('');
        } catch (error) {
            setError(error instanceof Error ? error.message : ERROR_MESSAGES.STOCK_NOT_FOUND);
        } finally {
            setIsLoading(false);
        }
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter' && !searchResults.length) {
            handleSearch();
        }
    };

    const renderExampleSearches = () => {
        if (!showExamples) return null;

        return (
            <div className="example-searches">
                <h4>Popular Searches:</h4>
                <div className="search-categories">
                    {Object.entries(COMMON_SEARCHES).map(([category, symbols]) => (
                        <div key={category} className="search-category">
                            <h5>{category.charAt(0).toUpperCase() + category.slice(1)}</h5>
                            <div className="symbol-list">
                                {symbols.map(symbol => (
                                    <button
                                        key={symbol}
                                        className="example-symbol"
                                        onClick={() => handleCompanySelect(symbol)}
                                    >
                                        {symbol}
                                    </button>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        );
    };

    return (
        <div className="stock-search">
            <div className="search-input-container">
                <input
                    type="text"
                    value={searchTerm}
                    onChange={handleInputChange}
                    onKeyPress={handleKeyPress}
                    placeholder="Enter stock symbol (e.g., AAPL) or company name (e.g., Apple)"
                    className="search-input"
                />
                <button
                    onClick={handleSearch}
                    disabled={isLoading}
                    className="search-button"
                >
                    {isLoading ? 'Searching...' : 'Search'}
                </button>
            </div>

            {error && <div className="error-message">{error}</div>}

            {searchResults.length > 0 && (
                <div className="search-results">
                    {searchResults.map((result) => (
                        <div
                            key={result.symbol}
                            className="search-result"
                            onClick={() => handleCompanySelect(result.symbol)}
                        >
                            <span className="symbol">{result.symbol}</span>
                            <span className="name">{result.name}</span>
                        </div>
                    ))}
                </div>
            )}

            {renderExampleSearches()}
        </div>
    );
}

StockSearch.propTypes = {
    onStockFound: PropTypes.func.isRequired
};

export default StockSearch;
