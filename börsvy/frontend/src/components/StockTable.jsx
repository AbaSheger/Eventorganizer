import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { StockShape } from '../types/Stock';
import './StockTable.css';

function StockTable({ stocks: initialStocks }) {
    const [stocks, setStocks] = useState(initialStocks);
    const [expandedStock, setExpandedStock] = useState(null);

    /**
     * Adds a stock to the favorites list by sending a POST request to the server.
     * Updates the local state to reflect the change if the request is successful.
     *
     * @async
     * @function addToFavorites
     * @param {string} symbol - The stock symbol to be added to the favorites list.
     * @returns {Promise<void>} - A promise that resolves when the operation is complete.
     * @throws {Error} Logs an error to the console if the request fails.
     *
     * @important Ensure the backend API is running at `http://localhost:8082`
     * and the endpoint `/api/stocks/favorites/:symbol` is correctly configured.
     *
     * @important This function assumes `stocks` is an array of stock objects
     * and `setStocks` is a state updater function available in the component's scope.
     */
    const addToFavorites = async (symbol) => {
        try {
            // Send a POST request to the backend API to add the stock to the favorites list
            await fetch(`http://localhost:8082/api/stocks/favorites/${symbol}`, {
                method: 'POST',
            });

            // Update the local state to mark the stock as a favorite
            const updatedStocks = stocks.map((s) =>
                s.symbol === symbol ? { ...s, favorite: true } : s
            );

            // Update the state with the modified stocks array
            setStocks(updatedStocks);
        } catch (error) {
            // Log an error message to the console if the request fails
            console.error('Error adding to favorites:', error);
        }
    };

    const removeFromFavorites = async (symbol) => {
        try {
            await fetch(`http://localhost:8082/api/stocks/favorites/${symbol}`, {
                method: 'DELETE',
            });
            const updatedStocks = stocks.map((s) =>
                s.symbol === symbol ? { ...s, favorite: false } : s
            );
            setStocks(updatedStocks);
        } catch (error) {
            console.error('Error removing from favorites:', error);
        }
    };

    const toggleExpand = (symbol) => {
        setExpandedStock(expandedStock === symbol ? null : symbol);
    };

    return (
        <div className="stock-table-container">
            <table className="stock-table">
                <thead>
                    <tr>
                        <th>Symbol</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Change</th>
                        <th>Sector</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {stocks.map((stock) => (
                        <React.Fragment key={stock.symbol}>
                            <tr className={expandedStock === stock.symbol ? 'expanded' : ''}>
                                <td>
                                    <button
                                        className="expand-button"
                                        onClick={() => toggleExpand(stock.symbol)}
                                    >
                                        {expandedStock === stock.symbol ? '▼' : '▶'}
                                    </button>
                                    {stock.symbol}
                                </td>
                                <td>{stock.name}</td>
                                <td className="price">${stock.price.toFixed(2)}</td>
                                <td
                                    className={`change ${stock.change >= 0 ? 'positive' : 'negative'}`}
                                >
                                    {stock.change >= 0 ? '+' : ''}
                                    {stock.change.toFixed(2)}%
                                </td>
                                <td>{stock.sector}</td>
                                <td>
                                    <button
                                        className={`favorite-button ${stock.favorite ? 'favorited' : ''}`}
                                        onClick={() =>
                                            stock.favorite
                                                ? removeFromFavorites(stock.symbol)
                                                : addToFavorites(stock.symbol)
                                        }
                                    >
                                        {stock.favorite ? '★' : '☆'}
                                    </button>
                                </td>
                            </tr>
                            {expandedStock === stock.symbol && (
                                <tr className="details-row">
                                    <td colSpan={6}>
                                        <div className="stock-details">
                                            <div className="detail-section">
                                                <h4>Company Information</h4>
                                                <p>{stock.description}</p>
                                            </div>
                                            <div className="detail-grid">
                                                <div>
                                                    <strong>Industry:</strong>
                                                    <p>{stock.industry}</p>
                                                </div>
                                                <div>
                                                    <strong>Website:</strong>
                                                    <p>
                                                        <a
                                                            href={stock.website}
                                                            target="_blank"
                                                            rel="noopener noreferrer"
                                                        >
                                                            {stock.website}
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            )}
                        </React.Fragment>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

StockTable.propTypes = {
    stocks: PropTypes.arrayOf(StockShape).isRequired,
};

export default StockTable;
