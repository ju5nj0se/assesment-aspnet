# Sistema de Gestión de Empleados (Employee Management System)

Sistema web robusto para la gestión integral de empleados, desarrollado con **ASP.NET Core 9** siguiendo una arquitectura limpia y profesional.

## 🚀 Características Principales
- **Arquitectura Limpia**: Separación de responsabilidades mediante el patrón **Repository-Service**.
- **Gestión de Empleados**: CRUD completo, generación de hojas de vida en PDF.
- **Importación Masiva**: Carga de usuarios desde archivos Excel con validaciones.
- **Seguridad**: Autenticación y autorización robusta con **ASP.NET Identity**.
- **API REST**: Endpoints seguros con autenticación JWT.
- **Documentación API**: Swagger integrado y configurado.

## 🛠️ Tecnologías Utilizadas
- **Backend**: .NET 9 (C#)
- **Base de Datos**: PostgreSQL (Entity Framework Core)
- **Frontend**: ASP.NET Core MVC (Razor Views) + Bootstrap
- **Contenedores**: Docker & Docker Compose
- **Librerías**: 
  - `EPPlus` (Excel)
  - `iText7` (PDF)
  - `Swashbuckle` (Swagger)

---

## 💻 Ejecución Local (Desarrollo)

### Prerrequisitos
- .NET SDK 9.0
- PostgreSQL instalado y corriendo.

### Pasos
1. **Configurar Base de Datos**:
   Ajusta la cadena de conexión en `appsettings.json` (`ConnectionStrings:DefaultConnection`).

2. **Ejecutar Migraciones**:
   ```bash
   dotnet ef database update
   ```

3. **Ejecutar la Aplicación**:
   ```bash
   dotnet run
   ```
   Accede a: `http://localhost:5093` (o el puerto indicado en consola).

---

## 🐳 Ejecución con Docker (Recomendado)

El proyecto incluye configuración completa para desplegarse junto con su base de datos usando Docker.

### Prerrequisitos
- Docker Desktop / Docker Engine
- Docker Compose

### Instrucciones Paso a Paso

1. **Construir y Desplegar**:
   Ejecuta el siguiente comando en la raíz del proyecto:
   ```bash
   docker-compose up --build -d
   ```

2. **Verificar**:
   Asegúrate de que los contenedores estén corriendo:
   ```bash
   docker-compose ps
   ```

3. **Acceder**:
   - **Aplicación Web**: [http://localhost:5000](http://localhost:5000)
   - **Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)

   > **Nota**: Las migraciones y la creación de datos semilla (usuario Admin) se ejecutan automáticamente al iniciar el contenedor.

4. **Credenciales por Defecto**:
   - **Admin App**: `admin@gmail.com` / `admin123`
   - **Base de Datos**: `postgres` / `admin123`

5. **Detener**:
   ```bash
   docker-compose down
   ```

---

## 📡 API Endpoints

La aplicación expone una API RESTful documentada.

### Autenticación
- **GET** `/api/auth/departments`: Listar departamentos (Público).
- **POST** `/api/auth/register`: Registrar nuevo empleado (Público).
- **POST** `/api/auth/login`: Login con Email + Documento (Retorna JWT).

### Empleados (Requiere Header `Authorization: Bearer <Token>`)
- **GET** `/api/me`: Obtener perfil completo del usuario autenticado.
- **GET** `/api/me/resume`: Descargar hoja de vida en PDF.

---

## 📂 Estructura del Proyecto

- **Controllers**: Controladores MVC y API (`/Api`).
- **Services**: Lógica de negocio (`/Implementations`, `/Interfaces`).
- **Data/Repositories**: Acceso a datos (`/Implementations`, `/Interfaces`).
- **DTOs**: Objetos de transferencia de datos.
- **Views**: Vistas Razor para el frontend administrativo.
