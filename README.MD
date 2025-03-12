
# Country Information Service

A .NET Core web API that integrates with a SOAP country info service, persists data to MySQL, and provides RESTful CRUD operations.

## Features

- 🚀 REST API endpoints for country information management
- 🔄 SOAP service integration (Oorsprong Country Info Service)
- 🐳 Docker & Docker Compose ready
- 📦 MySQL database integration
- 📄 Open API documentation
- 🔒 Error handling & logging

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [SoapUI](https://www.soapui.org/) (for SOAP testing)

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/country-info-service.git
cd country-info-service
```

### 2. Run with Docker Compose
```bash
docker-compose up --build
```

The application will be available at:  
🌐 `http://localhost:8080/swagger`

### 3. Verify Services
| Service        | Port  | URL                              |
|----------------|-------|----------------------------------|
| API            | 8080  | http://localhost:8080            |
| MySQL Database | 3306  | mysql://localhost:3306/countryinfo |
| Adminer        | 8081  | http://localhost:8081            |

## API Documentation

### Endpoints

#### 1. Get Country Information
```http
POST /api/country
```
**Request:**
```json
{
  "name": "tanzania"
}
```

**Response:**
```json
{
  "id": 1,
  "isoCode": "TZ",
  "name": "Tanzania",
  "capitalCity": "Dodoma",
  "phoneCode": "255",
  "continentCode": "AF",
  "currencyIsoCode": "TZS",
  "countryFlag": "🇹🇿"
}
```

#### 2. CRUD Operations
| Method | Endpoint                | Description          |
|--------|-------------------------|----------------------|
| GET    | /api/country            | Get all countries    |
| GET    | /api/country/{id}       | Get country by ID    |
| PUT    | /api/country/{id}       | Update country       |
| DELETE | /api/country/{id}       | Delete country       |



## Testing with SoapUI

1. Import the WSDL:
   ```
   http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?WSDL
   ```
2. Test SOAP endpoints:
   - `CountryISOCode`
   - `FullCountryInfo`

## Data Flow

1. Client sends country name to REST API
2. API converts name to proper case
3. API calls SOAP service for ISO code
4. API calls SOAP service for full country info
5. Data persisted to MySQL
6. CRUD operations available via REST API

## Development

### Run Locally
```bash
dotnet restore
dotnet run
```

### Database Migrations
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Troubleshooting

Common Issues:
- **Port conflicts**: Stop services using ports 8080, 3306
- **Database connection issues**:
  ```bash
  docker-compose logs db
  ```
- **Migration errors**:
  ```bash
  dotnet ef database drop --force
  dotnet ef migrations remove
  ```
