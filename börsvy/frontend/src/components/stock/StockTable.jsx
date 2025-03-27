import React from 'react';
import PropTypes from 'prop-types';
import { stockService } from '../../services/api';
import { ERROR_MESSAGES } from '../../constants';
import '../../styles/StockTable.css';

function StockTable({ stocks = [], onStockUpdate }) {
    const handleFavoriteToggle = async (stock) => {
        try {
            if (!stock || !stock.symbol) {
                throw new Error('Invalid stock data');
            }

            if (stock.isFavorite) {
                await stockService.removeFromFavorites(stock.symbol);
            } else {
                await stockService.addToFavorites(stock);
            }

            // Update the stock's favorite status in the parent component
            onStockUpdate({
                ...stock,
                isFavorite: !stock.isFavorite
            });
        } catch (error) {
            console.error('Error toggling favorite:', error);
            // Show a more specific error message
            alert(error.message || (stock.isFavorite ? ERROR_MESSAGES.REMOVE_FAILED : ERROR_MESSAGES.ADD_FAILED));
        }
    };

    if (!stocks || stocks.length === 0) {
        return (
            <div className="stock-table">
                <p>No stocks to display</p>
            </div>
        );
    }

    return (
        <div className="stock-table">
            <table>
                <thead>
                    <tr>
                        <th>Symbol</th>
                        <th>Price</th>
                        <th>Change</th>
                        <th>Change %</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {stocks.map((stock) => {
                        if (!stock || !stock.symbol) return null;
                        
                        return (
                            <tr key={stock.symbol}>
                                <td>{stock.symbol}</td>
                                <td>${(stock.price || 0).toFixed(2)}</td>
                                <td className={(stock.change || 0) >= 0 ? 'positive' : 'negative'}>
                                    ${Math.abs(stock.change || 0).toFixed(2)}
                                </td>
                                <td className={(stock.changePercent || 0) >= 0 ? 'positive' : 'negative'}>
                                    {(stock.changePercent || 0).toFixed(2)}%
                                </td>
                                <td>
                                    <button
                                        onClick={() => handleFavoriteToggle(stock)}
                                        className={`favorite-button ${stock.isFavorite ? 'favorited' : ''}`}
                                        title={stock.isFavorite ? 'Remove from favorites' : 'Add to favorites'}
                                    >
                                        {stock.isFavorite ? '★' : '☆'}
                                    </button>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}

StockTable.propTypes = {
    stocks: PropTypes.arrayOf(
        PropTypes.shape({
            symbol: PropTypes.string,
            price: PropTypes.number,
            change: PropTypes.number,
            changePercent: PropTypes.number,
            isFavorite: PropTypes.bool
        })
    ),
    onStockUpdate: PropTypes.func.isRequired
};

export default StockTable;
