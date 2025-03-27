import PropTypes from 'prop-types';

// This file now serves as a reference for the Stock object structure
// A stock object contains:
// - symbol (string)
// - name (string)
// - price (number)
// - change (number)
// - favorite (boolean)
// - description (string)
// - sector (string)
// - industry (string)
// - website (string)

// Define the Stock shape for prop-type validation
export const StockShape = PropTypes.shape({
    symbol: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    change: PropTypes.number.isRequired,
    favorite: PropTypes.bool.isRequired,
    description: PropTypes.string,
    sector: PropTypes.string,
    industry: PropTypes.string,
    website: PropTypes.string,
});

// Example of how to create a Stock object
export const createStock = ({
    symbol,
    name,
    price,
    change,
    favorite = false,
    description = '',
    sector = '',
    industry = '',
    website = '',
}) => ({
    symbol,
    name,
    price,
    change,
    favorite,
    description,
    sector,
    industry,
    website,
});
