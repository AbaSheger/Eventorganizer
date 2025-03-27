const API_URL = process.env.REACT_APP_API_URL;

export const stockService = {
    async searchStock(query) {
        // Check if query is a stock symbol (uppercase letters only)
        const isSymbol = /^[A-Z]+$/.test(query);
        const endpoint = isSymbol ? 'symbol' : 'company';
        
        const response = await fetch(`${API_URL}/api/stocks/search/${endpoint}/${encodeURIComponent(query)}`);
        if (!response.ok) {
            throw new Error('Stock not found');
        }
        return response.json();
    },

    async searchCompanies(query) {
        try {
            const response = await fetch(`${API_URL}/api/stocks/search/company/${encodeURIComponent(query)}`);
            if (!response.ok) {
                return []; // Return empty array for any error
            }
            const data = await response.json();
            return data || [];
        } catch (error) {
            console.error('Company search error:', error);
            return []; // Return empty array instead of throwing
        }
    },

    async getFavorites() {
        const response = await fetch(`${API_URL}/api/stocks/favorites`);
        if (!response.ok) {
            throw new Error('Failed to fetch favorites');
        }
        return response.json();
    },

    async addToFavorites(stock) {
        try {
            // Ensure we have the required data
            if (!stock || !stock.symbol) {
                throw new Error('Invalid stock data');
            }

            const response = await fetch(`${API_URL}/api/stocks/favorites`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    symbol: stock.symbol,
                    name: stock.name,
                    price: stock.price,
                    change: stock.change,
                    changePercent: stock.changePercent,
                    isFavorite: true
                }),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.error || 'Failed to add stock to favorites');
            }

            return response.json();
        } catch (error) {
            console.error('Error adding to favorites:', error);
            throw error;
        }
    },

    async removeFromFavorites(symbol) {
        const response = await fetch(`${API_URL}/api/stocks/favorites/${symbol}`, {
            method: 'DELETE',
        });
        if (!response.ok) {
            throw new Error('Failed to remove stock from favorites');
        }
        return response.json();
    },
};
