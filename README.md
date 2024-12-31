# FinanzautoTechnicalTestDotNet

API desarrollada en **.NET 8** utilizando **Entity Framework Core**. El objetivo es gestionar la información de estudiantes, profesores, cursos y calificaciones, con autenticación basada en JWT.

---

## Características

- CRUD completo para:
  - **Estudiantes**
  - **Profesores**
  - **Cursos**
  - **Calificaciones**
- Autenticación basada en **JWT**.
- Autorización de endpoints mediante el atributo `[Authorize]`.
- Lógica de autenticación conectada a la base de datos.
- Arquitectura modular con patrón **Repository**.

---

## Tecnologías utilizadas

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **JWT (JSON Web Token)** para autenticación
- **C#**

---

## Configuración inicial

### 1. Clonar el repositorio


### 2. Configurar la base de datos

1. Crear una base de datos en SQL Server.
2. Ejecutar scripts enviados


### 3. Configurar el archivo `appsettings.json`

Actualiza la cadena de conexión en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=<SERVIDOR>;Database=<BASE_DE_DATOS>;User Id=<USUARIO>;Password=<CONTRASEÑA>;"
}
```