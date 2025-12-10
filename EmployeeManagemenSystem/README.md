# Employee Management System

A robust web system for integral employee management, developed with **ASP.NET Core 9** following a clean and professional architecture.

## 🚀 Key Features
- **Clean Architecture**: Separation of concerns using the **Repository-Service** pattern.
- **Employee Management**: Full CRUD, PDF resume generation.
- **Bulk Import**: Load users from Excel files with validation.
- **Security**: Robust authentication and authorization with **ASP.NET Identity**.
- **REST API**: Secure endpoints with JWT authentication.
- **API Documentation**: Integrated and configured Swagger.

## 🛠️ Technology Stack
- **Backend**: .NET 9 (C#)
- **Database**: PostgreSQL (Entity Framework Core)
- **Frontend**: ASP.NET Core MVC (Razor Views) + Bootstrap
- **Containers**: Docker & Docker Compose
- **Libraries**: 
  - `EPPlus` (Excel)
  - `iText7` (PDF)
  - `Swashbuckle` (Swagger)

---

## 💻 Local Execution (Development)

### Prerequisites
- .NET SDK 9.0
- PostgreSQL installed and running.

### Steps
1. **Configure Database**:
   Adjust the connection string in `appsettings.json` (`ConnectionStrings:DefaultConnection`).

2. **Run Migrations**:
   ```bash
   dotnet ef database update
   ```

3. **Run Application**:
   ```bash
   dotnet run
   ```
   Access at: `http://localhost:5093` (or the port indicated in the console).

---

## 🐳 Docker Execution (Recommended)

The project includes full configuration to be deployed along with its database using Docker.

### Prerequisites
- Docker Desktop / Docker Engine
- Docker Compose (v2 recommended)

### Step-by-Step Instructions

1. **Build and Deploy**:
   Run the following command in the project root:
   ```bash
   docker compose up --build -d
   ```

2. **Verify**:
   Ensure containers are running:
   ```bash
   docker compose ps
   ```

3. **Access**:
   - **Web Application**: [http://localhost:5000](http://localhost:5000)
   - **Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - **Database External Access**: Port `3030` (Host) -> `5432` (Container)

   > **Note**: Migrations and data seeding (Admin user, Catalog data) are executed automatically when the container starts.

4. **Default Credentials**:
   - **App Admin**: `admin@gmail.com` / `admin123`
   - **Database**: `postgres` / `admin123`

5. **Stop**:
   ```bash
   docker compose down
   ```
   *To stop and clean up volumes (resets DB):* `docker compose down -v`

---

## 📡 API Endpoints

The application exposes a documented RESTful API.

### Authentication
- **GET** `/api/auth/departments`: List departments (Public).
- **POST** `/api/auth/register`: Register new employee (Public).
- **POST** `/api/auth/login`: Login with Email + Document (Returns JWT).

### Employees (Requires Header `Authorization: Bearer <Token>`)
- **GET** `/api/me`: Get full profile of authenticated user.
- **GET** `/api/me/resume`: Download resume in PDF.

---

## 📂 Project Structure

- **Controllers**: MVC and API Controllers (`/Api`).
- **Services**: Business Logic (`/Implementations`, `/Interfaces`).
- **Data/Repositories**: Data Access (`/Implementations`, `/Interfaces`).
- **DTOs**: Data Transfer Objects.
- **Views**: Razor Views for the administrative frontend.
