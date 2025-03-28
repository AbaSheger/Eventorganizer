import React, { useState, useEffect } from 'react';
import StockSearch from './components/stock/StockSearch';
import StockTable from './components/stock/StockTable';
import { stockService } from './services/api';
import { APP_NAME, APP_DESCRIPTION } from './constants';

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
            setStocks(favorites.map((stock) => ({ ...stock, isFavorite: true })));
        } catch (error) {
            console.error('Error fetching favorites:', error);
            setError(error.message);
        } finally {
            setIsLoading(false);
        }
    };

    const handleStockFound = (stock) => {
        setStocks((prevStocks) => {
            const exists = prevStocks.some((s) => s.symbol === stock.symbol);
            if (exists) {
                return prevStocks.map((s) =>
                    s.symbol === stock.symbol ? { ...stock, isFavorite: s.isFavorite } : s
                );
            }
            return [{ ...stock, isFavorite: false }, ...prevStocks];
        });
    };

    const handleStockUpdate = (updatedStock) => {
        setStocks((prevStocks) =>
            prevStocks.map((stock) => (stock.symbol === updatedStock.symbol ? updatedStock : stock))
        );
    };

    if (isLoading) {
        return (
            <div className="min-h-screen bg-bg-secondary p-4">
                <div className="w-full max-w-4xl mx-auto mt-10 p-6 bg-white rounded-lg shadow-md text-center">
                    <div className="animate-pulse text-text-primary">
                        Loading your favorite stocks...
                    </div>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="min-h-screen bg-bg-secondary p-4">
                <div className="w-full max-w-4xl mx-auto mt-10 p-6 bg-white rounded-lg shadow-md">
                    <div className="p-4 bg-negative-red/10 border border-negative-red/30 rounded-md text-negative-red">
                        Error: {error}
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-bg-secondary">
            <header className="sticky top-0 bg-white shadow-sm z-10 p-4">
                <div className="w-full max-w-4xl mx-auto">
                    <h1 className="text-xl font-semibold text-primary-blue">{APP_NAME}</h1>
                    <p className="text-sm text-text-secondary">{APP_DESCRIPTION}</p>
                </div>
            </header>

            <main className="container mx-auto max-w-4xl p-4 space-y-6">
                <StockSearch onStockFound={handleStockFound} />

                {stocks.length > 0 ? (
                    <StockTable stocks={stocks} onStockUpdate={handleStockUpdate} />
                ) : (
                    <div className="w-full p-8 bg-white rounded-lg shadow-md text-center">
                        <p className="text-text-secondary">
                            No stocks added yet. Search for a stock symbol above to get started!
                        </p>
                    </div>
                )}
            </main>
        </div>
    );
}

export default App;
