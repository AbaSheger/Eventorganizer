import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { stockService } from '../../services/api';
import { EXAMPLE_SYMBOLS, ERROR_MESSAGES } from '../../constants';
// Common stock search patterns
const COMMON_SEARCHES = {
    tech: ['AAPL', 'GOOGL', 'MSFT', 'META', 'NVDA', 'AMD', 'INTC', 'CRM', 'ADBE', 'PYPL'],
    finance: ['JPM', 'BAC', 'V', 'MA', 'GS', 'MS', 'BLK', 'AXP', 'WFC', 'SCHW'],
    healthcare: ['JNJ', 'PFE', 'MRK', 'ABBV', 'UNH', 'ABT', 'BMY', 'AMGN', 'GILD', 'MRNA'],
    retail: ['WMT', 'AMZN', 'TGT', 'HD', 'COST', 'LOW', 'MCD', 'SBUX', 'NKE', 'TJX'],
    energy: ['XOM', 'CVX', 'COP', 'SLB', 'EOG', 'MPC', 'PSX', 'VLO', 'OXY', 'PXD'],
    automotive: ['TSLA', 'F', 'GM', 'TM', 'HMC', 'STLA', 'RIVN', 'LCID', 'NIO', 'XPEV'],
    popular: ['AAPL', 'GOOGL', 'MSFT', 'AMZN', 'TSLA', 'NVDA', 'META', 'JPM', 'V', 'WMT'],
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

    // New function to just fill the search input without performing search
    const fillSearchInput = (symbol) => {
        setSearchTerm(symbol);
        setSearchResults([]);
        setShowExamples(false);
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter' && !searchResults.length) {
            handleSearch();
        }
    };

    const renderExampleSearches = () => {
        if (!showExamples) return null;
        return (
            <div className="mt-4">
                <h4 className="text-sm font-medium text-text-secondary mb-2">Popular Searches:</h4>
                <div className="space-y-4">
                    {Object.entries(COMMON_SEARCHES).map(([category, symbols]) => (
                        <div key={category} className="rounded-lg bg-bg-secondary p-3">
                            <h5 className="text-sm font-medium text-text-primary mb-2">
                                {category.charAt(0).toUpperCase() + category.slice(1)}
                            </h5>
                            <div className="flex flex-wrap gap-2">
                                {symbols.map((symbol) => (
                                    <button
                                        key={symbol}
                                        className="px-3 py-1 text-sm rounded-full bg-bg-tertiary hover:bg-border-light text-text-primary transition-colors"
                                        onClick={() => fillSearchInput(symbol)}
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
        <div className="w-full p-4 bg-white rounded-lg shadow-md">
            <div className="relative">
                <input
                    type="text"
                    value={searchTerm}
                    onChange={handleInputChange}
                    onKeyPress={handleKeyPress}
                    placeholder="Enter stock symbol (e.g., AAPL) or company name (e.g., Apple)"
                    className="w-full px-4 py-3 rounded-lg border border-border-light focus:outline-none focus:ring-2 focus:ring-primary-light focus:border-transparent"
                />
                <button
                    onClick={handleSearch}
                    disabled={isLoading}
                    className="absolute right-2 top-1/2 transform -translate-y-1/2 px-4 py-1.5 bg-primary-blue text-white rounded-md hover:bg-primary-dark transition-colors disabled:bg-neutral-gray disabled:cursor-not-allowed"
                >
                    {isLoading ? 'Searching...' : 'Search'}
                </button>
            </div>

            {error && (
                <div className="mt-2 px-3 py-2 bg-negative-red/10 border border-negative-red/30 rounded-md text-negative-red text-sm">
                    {error}
                </div>
            )}

            {searchResults.length > 0 && (
                <div className="mt-3 border border-border-light rounded-lg overflow-hidden divide-y divide-border-light">
                    {searchResults.map((result) => (
                        <div
                            key={result.symbol}
                            className="p-3 hover:bg-bg-secondary cursor-pointer transition-colors"
                            onClick={() => handleCompanySelect(result.symbol)}
                        >
                            <span className="font-medium text-text-primary mr-2">
                                {result.symbol}
                            </span>
                            <span className="text-text-secondary text-sm">{result.name}</span>
                        </div>
                    ))}
                </div>
            )}

            {renderExampleSearches()}
        </div>
    );
}

StockSearch.propTypes = {
    onStockFound: PropTypes.func.isRequired,
};

export default StockSearch;
