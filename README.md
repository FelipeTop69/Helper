# Para un Correcto Funcionamiento de los Modelos

### Backend

#### Inicializar
```bash
dotnet restore
dotnet build
dotnet run
```
#### Comandos de Package Necesarios

```bash
# Entity
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.4
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.4
# Provedores DB
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.4
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
# Para el Provedor de MySql buscar la version preeliminar en el IDE
# Para Migracion
dotnet add package Microsoft.EntityFrameworkCore.Design (Permite Comandos de Migracion)

# Business
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1

# Web
dotnet add package Swashbuckle.AspNetCore --version 8.1.1
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.4

```

### Frontend

#### Inicializar
```bash
npm install
ng serve
```

#### Comandos Angular CLI

```bash
# CrearProyecto
ng new nombre-proyecto

# Ejecutar APP
ng serve -o

# Angular Material
ng add @angular/material

# Generar Ambientes
ng g environments

# SweetAler
npm install sweetalert2
```
