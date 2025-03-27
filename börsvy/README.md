# BörsVy - Real-time Stock Market Tracker

A graduation project demonstrating a real-time stock market tracking application built with React and Spring Boot.

## Features

- Real-time stock price tracking
- Search functionality for stock symbols
- Favorite stocks management
- Responsive design
- Real-time price updates

## Tech Stack

- Frontend: React.js
- Backend: Spring Boot
- API: Alpha Vantage (Stock Market Data)

## Prerequisites

- Node.js (v14 or higher)
- Java JDK 17 or higher
- Maven

## Getting Started

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The application will be available at `http://localhost:3000`

### Backend Setup

1. Navigate to the backend directory:
```bash
cd backend
```

2. Build the project:
```bash
mvn clean install
```

3. Run the application:
```bash
mvn spring-boot:run
```

The API will be available at `http://localhost:8082`

## API Endpoints

- `GET /api/stocks/search/{symbol}` - Search for a stock
- `GET /api/stocks/favorites` - Get favorite stocks
- `POST /api/stocks/favorites` - Add a stock to favorites
- `DELETE /api/stocks/favorites/{symbol}` - Remove a stock from favorites

## Demo Instructions

1. Start both frontend and backend servers
2. Open the application in your browser
3. Search for a stock symbol (e.g., AAPL, MSFT, GOOGL)
4. The stock will be added to your favorites list
5. Real-time price updates will be displayed

## Project Structure

```
borsvy/
├── frontend/           # React frontend application
│   ├── src/
│   │   ├── components/    # React components
│   │   ├── types/        # Type definitions
│   │   └── App.jsx       # Main application component
│   └── package.json
└── backend/           # Spring Boot backend application
    └── src/
        └── main/
            └── java/
                └── com/borsvy/
                    ├── controller/    # REST controllers
                    ├── service/       # Business logic
                    └── model/         # Data models
```

## Author

[Abenezer Anglo]

## License

This project is licensed under the MIT License - see the LICENSE file for details. 