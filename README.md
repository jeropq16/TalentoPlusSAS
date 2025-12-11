
---

# Sistema de Gestión de Empleados – TalentoPlus S.A.S.

## 📌 Descripción del Proyecto
TalentoPlus S.A.S. moderniza su área de Recursos Humanos con una solución integral compuesta por:

- **Aplicación Web (ASP.NET Core MVC)** para el administrador de RRHH.
- **API REST (ASP.NET Core Web API)** para empleados.
- **Base de Datos Mysql** centralizada.
- **Dashboard con Inteligencia Artificial** para consultas en lenguaje natural.
- **Gestión de empleados** con CRUD, importación desde Excel y generación de Hoja de Vida en PDF.

La solución está diseñada bajo principios de **Clean Architecture / DDD**, con separación clara entre capas de presentación, dominio e infraestructura.

---

## 🛠️ Tecnologías Utilizadas
- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **Mysql**
- **Docker & Docker Compose**
- **Gemini / ChatGPT API** para consultas de IA
- **SMTP (MailKit)** para envío de correos reales
- **xUnit** para pruebas unitarias e integración

---

## ⚙️ Arquitectura
```
src/
 ├── WebApp/                # Aplicación Web MVC (Administrador RRHH)
 ├── Api/                   # API REST para empleados
 ├── Domain/                # Entidades y lógica de negocio
 ├── Infrastructure/        # Persistencia, repositorios, servicios externos
 ├── Application/           # Casos de uso, DTOs, servicios de aplicación
 └── Tests/                 # Pruebas unitarias e integración
```

---

## 🚀 Pasos para levantar la solución con Docker

### 1. Clonar el repositorio
```bash
git clone https:https://github.com/jeropq16/TalentoPlus
cd TalentoPlus
```

### 2. Configurar variables de entorno
Crear un archivo `.env` en la raíz del proyecto con el siguiente contenido:

```env
# Base de datos
MYSQL_USER=admin
MYSQL_PASSWORD=admin123
MYSQL_DB=talentoplusdb

# Connection string
DB_CONNECTION=Host=db;Database=talentoplusdb;Username=admin;Password=admin123

# JWT
JWT_SECRET=supersecreto12345
JWT_ISSUER=talentoplus-api
JWT_AUDIENCE=talentoplus-clients

# SMTP
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
SMTP_USER=tuemail@gmail.com
SMTP_PASS=tucontraseña
```

### 3. Levantar los contenedores
```bash
docker-compose up --build
```

Esto levantará:
- **db** → MySQL
- **web** → Aplicación Web MVC
- **api** → API REST

### 4. Acceder a la aplicación
- 🌐 **WebApp:** [http://localhost:5000](http://localhost:5000)  
- 🔗 **API REST:** [http://localhost:5001](http://localhost:5001)  

---

## 🔑 Credenciales de acceso

### Administrador RRHH
- **Usuario:** admin@talentoplus.com  
- **Contraseña:** Admin123!  

### Empleado (ejemplo)
- **Documento:** 123456789  
- **Correo:** empleado@talentoplus.com  
- **Contraseña:** Empleado123!  

---

## 📊 Funcionalidades principales

### Aplicación Web
- CRUD de empleados y departamentos.
- Importación de empleados desde Excel.
- Generación de Hoja de Vida en PDF.
- Dashboard con métricas:
  - Total de empleados.
  - Empleados en vacaciones.
  - Empleados activos por departamento.
- Consultas en lenguaje natural con IA.

### API REST
**Endpoints públicos:**
- `GET /api/departamentos` → Listar departamentos.
- `POST /api/empleados/registro` → Autoregistro con envío de correo.
- `POST /api/auth/login` → Login y obtención de JWT.

**Endpoints protegidos (JWT):**
- `GET /api/empleados/me` → Consultar información personal.
- `GET /api/empleados/me/pdf` → Descargar Hoja de Vida en PDF.

---

## 🧪 Pruebas Automatizadas
Ejecutar pruebas:
```bash
dotnet test
```

Incluye:
- **Unitarias:** Validación de entidades y servicios.
- **Integración:** Endpoints de API y conexión a Mysql.

---

## 📂 Entregables
- Código fuente completo.
- Archivo Excel de empleados (`empleados.xlsx`).
- README con pasos de ejecución y configuración.
- Docker Compose para despliegue.

---

## 🔗 Repositorio
👉 https:https://github.com/jeropq16/TalentoPlus

---
