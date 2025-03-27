import React, { useState, useEffect } from 'react';
import StockSearch from './components/stock/StockSearch';
import StockTable from './components/stock/StockTable';
import { stockService } from './services/api';
import { APP_NAME, APP_DESCRIPTION } from './constants';
import './styles/App.css';

function App() {
    const [stocks, setStocks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchFavorites();
    }, []);

    const fetchFavorites = async () => {
        try {
            setIsLoading(true);
            const favorites = await stockService.getFavorites();
            setStocks(favorites.map(stock => ({ ...stock, isFavorite: true })));
        } catch (error) {
            console.error('Error fetching favorites:', error);
            setError(error.message);
        } finally {
            setIsLoading(false);
        }
    };

    const handleStockFound = (stock) => {
        setStocks(prevStocks => {
            const exists = prevStocks.some(s => s.symbol === stock.symbol);
            if (exists) {
                return prevStocks.map(s => s.symbol === stock.symbol ? { ...stock, isFavorite: s.isFavorite } : s);
            }
            return [{ ...stock, isFavorite: false }, ...prevStocks];
        });
    };

    const handleStockUpdate = (updatedStock) => {
        setStocks(prevStocks =>
            prevStocks.map(stock =>
                stock.symbol === updatedStock.symbol ? updatedStock : stock
            )
        );
    };

    if (isLoading) {
        return (
            <div className="app">
                <div className="loading">Loading your favorite stocks...</div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="app">
                <div className="error">Error: {error}</div>
            </div>
        );
    }

    return (
        <div className="app">
            <header className="app-header">
                <h1>{APP_NAME}</h1>
                <p>{APP_DESCRIPTION}</p>
            </header>
            <main className="app-main">
                <StockSearch onStockFound={handleStockFound} />
                {stocks.length > 0 ? (
                    <StockTable 
                        stocks={stocks} 
                        onStockUpdate={handleStockUpdate}
                    />
                ) : (
                    <div className="empty-state">
                        <p>No stocks added yet. Search for a stock symbol above to get started!</p>
                    </div>
                )}
            </main>
        </div>
    );
}

export default App;
