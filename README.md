# PocketCard API

A .NET 10.0 Web API for managing users, accounts, and cards.

## Prerequisites

- Docker
- Docker Compose

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd PocketCard
   ```

2. **Build and run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

3. **Access the API**
   - API: http://localhost:5225
   - Swagger UI: http://localhost:5225/swagger

## API Endpoints

### Users
- `POST /api/users/register` - Register a new user
- `POST /api/users/login` - Login user
- `GET /api/users/{username}` - Get user by username

### Accounts
- `POST /api/accounts` - Create account
- `GET /api/accounts/{id}` - Get account by ID
- `GET /api/accounts/user/{userId}` - Get accounts by user
- `PUT /api/accounts/{id}` - Update account
- `DELETE /api/accounts/{id}` - Delete account

### Cards
- `POST /api/cards` - Create card
- `GET /api/cards/{id}` - Get card by ID
- `GET /api/cards/user/{userId}` - Get cards by user
- `GET /api/cards/account/{accountId}` - Get cards by account
- `PUT /api/cards/{id}` - Update card
- `DELETE /api/cards/{id}` - Delete card
- `GET /api/cards/validate/{id}` - Validate card

### Health Check
- `GET /health` - Health check endpoint

## Development

### Local Development
1. Ensure you have .NET 10.0 SDK installed
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   cd PocketCard.Api
   dotnet run
   ```

### Docker Commands

```bash
# Build the image
docker build -t pocketcard-api .

# Run the container
docker run -p 5225:5225 pocketcard-api

# Run with Docker Compose
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f

# Rebuild and restart
docker-compose up --build --force-recreate
```

## Environment Variables

The application supports the following environment variables:

- `Database__Path` - SQLite database file path (default: `/app/data/pocketcard.db`)
- `Jwt__Issuer` - JWT token issuer
- `Jwt__Key` - JWT signing key
- `ASPNETCORE_ENVIRONMENT` - Environment (Development/Production)
- `ASPNETCORE_URLS` - URLs the application listens on

## Database

The application uses SQLite for data persistence. In Docker, the database file is stored in a named volume `pocketcard-db` for persistence across container restarts.

## Architecture

- **PocketCard.Api** - Web API layer with endpoints
- **PocketCard.UseCases** - Application logic and business rules
- **PocketCard.Domain** - Domain entities and business models
- **PocketCard.Infrastructure** - Data access and external services

## Technologies

- .NET 10.0
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- MediatR (CQRS pattern)
- JWT Authentication
- Swagger/OpenAPI
- Docker