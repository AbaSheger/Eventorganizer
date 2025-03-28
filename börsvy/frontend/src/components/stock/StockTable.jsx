import React from 'react';
import PropTypes from 'prop-types';
import { stockService } from '../../services/api';
import { ERROR_MESSAGES } from '../../constants';

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
                isFavorite: !stock.isFavorite,
            });
        } catch (error) {
            console.error('Error toggling favorite:', error);
            // Show a more specific error message
            alert(
                error.message ||
                    (stock.isFavorite ? ERROR_MESSAGES.REMOVE_FAILED : ERROR_MESSAGES.ADD_FAILED)
            );
        }
    };

    if (!stocks || stocks.length === 0) {
        return (
            <div className="w-full p-4 bg-white rounded-lg shadow-md text-center">
                <p className="text-text-secondary">No stocks to display</p>
            </div>
        );
    }

    return (
        <div className="w-full overflow-hidden bg-white rounded-lg shadow-md">
            <table className="w-full">
                <thead className="bg-bg-secondary border-b border-border-light">
                    <tr>
                        <th className="py-3 px-4 text-left text-sm font-semibold text-text-secondary">
                            Symbol
                        </th>
                        <th className="py-3 px-4 text-right text-sm font-semibold text-text-secondary">
                            Price
                        </th>
                        <th className="py-3 px-4 text-right text-sm font-semibold text-text-secondary">
                            Change
                        </th>
                        <th className="py-3 px-4 text-right text-sm font-semibold text-text-secondary">
                            Change %
                        </th>
                        <th className="py-3 px-4 text-center text-sm font-semibold text-text-secondary">
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-border-light">
                    {stocks.map((stock) => {
                        if (!stock || !stock.symbol) return null;

                        const isPositiveChange = (stock.change || 0) >= 0;
                        const isPositivePercent = (stock.changePercent || 0) >= 0;

                        return (
                            <tr
                                key={stock.symbol}
                                className="hover:bg-bg-secondary transition-colors"
                            >
                                <td className="py-4 px-4 font-medium text-text-primary">
                                    {stock.symbol}
                                </td>
                                <td className="py-4 px-4 text-right font-medium text-text-primary">
                                    ${(stock.price || 0).toFixed(2)}
                                </td>
                                <td
                                    className={`py-4 px-4 text-right font-medium ${isPositiveChange ? 'text-positive-green' : 'text-negative-red'}`}
                                >
                                    {isPositiveChange ? '+' : '-'}$
                                    {Math.abs(stock.change || 0).toFixed(2)}
                                </td>
                                <td
                                    className={`py-4 px-4 text-right font-medium ${isPositivePercent ? 'text-positive-green' : 'text-negative-red'}`}
                                >
                                    {isPositivePercent ? '+' : '-'}
                                    {Math.abs(stock.changePercent || 0).toFixed(2)}%
                                </td>
                                <td className="py-4 px-4 text-center">
                                    <button
                                        onClick={() => handleFavoriteToggle(stock)}
                                        className={`p-2 rounded-full transition-colors ${
                                            stock.isFavorite
                                                ? 'text-primary-blue bg-primary-light/20'
                                                : 'text-text-tertiary hover:bg-bg-tertiary'
                                        }`}
                                        title={
                                            stock.isFavorite
                                                ? 'Remove from favorites'
                                                : 'Add to favorites'
                                        }
                                    >
                                        <span className="text-xl">
                                            {stock.isFavorite ? '★' : '☆'}
                                        </span>
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
            isFavorite: PropTypes.bool,
        })
    ),
    onStockUpdate: PropTypes.func.isRequired,
};

export default StockTable;
