# 🎓 WebducationApi

### Sistema de Gestión Educativa | .NET 8 + SQL Server + Arquitectura Hexagonal

---

## 📌 Descripción del Proyecto

**WebducationApi** es una API RESTful desarrollada con .NET 8 que implementa un sistema de gestión académica para estudiantes y docentes. El proyecto permite realizar operaciones **CRUD completas** (crear, leer, actualizar, eliminar) utilizando **procedimientos almacenados**, conexión a **SQL Server** y diseño basado en **arquitectura hexagonal**.

Su estructura promueve una alta cohesión y bajo acoplamiento entre capas, facilitando mantenibilidad, escalabilidad y pruebas.

---

## 🏗️ Arquitectura del Proyecto

```plaintext
├── Application               # Servicios, interfaces y DTOs
├── Domain                   # Entidades del dominio
├── Infrastructure           # Conexión a la base de datos con SqlClient + Dapper
├── Transversal              # AutoMapper, Logging, Helpers
├── WebducationApi           # WebAPI principal con controladores
⚙️ Tecnologías Utilizadas
✅ .NET 8

✅ SQL Server

✅ Dapper para acceso a datos

✅ BCrypt para encriptación de contraseñas

✅ JWT para autenticación y autorización

✅ Swagger para pruebas y documentación de endpoints

✅ AutoMapper, IOptions, y buenas prácticas de configuración

✅ Arquitectura Hexagonal (Ports & Adapters)

🧱 Funcionalidades Principales
🔐 Inicio de sesión con autenticación JWT (token Bearer)

👨‍🎓 CRUD completo para Estudiantes

👨‍🏫 CRUD completo para Docentes (en progreso o planificado)

🧮 Control de Calificaciones y Cursos (estructurado desde la BD)

📃 Consumo y prueba de endpoints desde Swagger UI

🔐 Seguridad
Contraseñas hasheadas con BCrypt

Protección de rutas con JWT Bearer Token

Usuario inicial (superAdmin) definido por configuración segura vía user-secrets

🗃️ Base de Datos
Este sistema utiliza procedimientos almacenados y objetos definidos en SQL Server. No usa Entity Framework.

⚠️ Importante
Los scripts SQL (DDL, DML y SPs) no están embebidos en el código, sino que deben descargarse de forma separada:

📁 ¿Cómo obtenerlos?
Clonando el repositorio completo:

bash
git clone https://github.com/halcondorado123/WebducationApi.git
O descargando como ZIP desde GitHub, y extrayendo la carpeta Database.

🔌 Configuración de Conexión
Edita el archivo appsettings.json con los datos correctos de tu servidor:

json
"ConnectionStrings": {
  "MyLocalConnection": "Server=(RedLocal);Database=SCHOOL_DB;Trusted_Connection=True;TrustServerCertificate=True"
}
Asegúrate de que la base de datos SCHOOL_DB ya esté creada y contenga las tablas y procedimientos necesarios.

🚀 Ejecutar el Proyecto
Configura user-secrets para el usuario inicial:

bash
dotnet user-secrets init
dotnet user-secrets set "InitUser:UserName" "superAdmin"
dotnet user-secrets set "InitUser:Password" "tupasswordsegura"
Restaura dependencias:

bash
dotnet restore
Ejecuta la API:

bash
dotnet run --project WebducationApi/WebducationApi.csproj
Abre Swagger en el navegador:

bash
https://localhost:{puerto}/swagger
🧪 Autenticación en Swagger
Realiza un POST a /api/token con tu usuario y contraseña.

Copia el token JWT retornado.

Haz clic en Authorize en la parte superior de Swagger.

Pega el token:
Ahora puedes consumir endpoints protegidos.

👤 Autor
Jhonattan Halcón Casallas Felipe
📧 falconfelipedeveloper@gmail.com
🌐 https://briefcase-jhonattancasallas.web.app
🔗 GitHub: @halcondorado123

✅ Estado del Proyecto
🟢 Activo — Se encuentra en desarrollo y pruebas, con funcionalidades CRUD para estudiantes ya implementadas y autenticación segura en producción.
