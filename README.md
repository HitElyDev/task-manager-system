# Sistema de Gestión de Tareas (Task Manager System)

Este proyecto implementa una API REST para la gestión de tareas, desarrollada utilizando *.NET 10*  y siguiendo los principios de la *Arquitectura Limpia (Clean Architecture)*.

El objetivo principal es demostrar la aplicación de *patrones de diseño, **buenas prácticas SOLID* y la configuración de un Pipeline completo de *CI/CD* con Azure DevOps.

---

## 1. 🏗️ Justificación de Diseño y Arquitectura

La solución está estructurada en cuatro capas, separando claramente las responsabilidades (Principio S de SOLID) para lograr un alto *desacoplamiento*.

### Principios y Patrones Aplicados

* *Arquitectura de Cebolla (Clean Architecture):* El diseño se centra en el *Dominio*, aislando la lógica de negocio de los detalles tecnológicos.
* *Inversión de Dependencia (DIP):* Los módulos de alto nivel (Servicios de Aplicación) dependen de *Abstracciones* (Interfaces), no de implementaciones concretas.
* *Patrón Repository y Unidad de Trabajo (UoW):* Se utiliza IHTaskRepository y IUnitOfWork (definidos en el *Domain*) para aislar la lógica de negocio de la implementación de acceso a datos.

### ⚠️ Justificación de Cambio de Persistencia (OCP / Portabilidad)

> *Cambio de SQL Server a SQLite:* En el entorno local de desarrollo (Build), se utilizó *SQL Server* para las pruebas iniciales. Sin embargo, para el despliegue en el servicio de Azure App Service, se realizó un cambio estratégico a *SQLite. Esta decisión se tomó para **garantizar el despliegue exitoso bajo las restricciones de la suscripción de Azure* para servicios SQL costosos.
> 
>Este cambio demuestra la aplicación del *Principio Abierto/Cerrado (OCP)* y la *portabilidad* de la arquitectura: el cambio de base de datos (una modificación en la capa *Infrastructure) no requirió **ninguna alteración* en las capas de *Application* o *Domain*.

### Tecnología Base

* *Framework:* .NET 10 / ASP.NET Core
* *Persistencia:* Entity Framework Core con *SQLite* (Base de datos basada en archivos, TaskManager.db).
* *Contratos:* Swagger/OpenAPI para la documentación de endpoints.

---

## 2. ⚙️ Instrucciones de Construcción y Ejecución

### A. Prerrequisitos

* .NET SDK (versión 10 o superior).
* Herramienta dotnet-ef instalada globalmente (dotnet tool install --global dotnet-ef).

### B. Ejecución Local (Clonar y Construir)

1.  *Clonar el Repositorio:*
    bash
    git clone https://github.com/HitElyDev/task-manager-system.git
    cd task-manager-system
    
2.  *Restaurar Dependencias:*
    bash
    dotnet restore
    
3.  *Configurar Base de Datos (Migrations - SQLite):*
    * Nota: Se asume que el cambio de proveedor a SQLite en Program.cs ya se realizó.
    bash
    # 1. Agregar la migración inicial (o la nueva, si la anterior fue SQL Server)
    dotnet ef migrations add InitialMigrationSQLite --project HTask.Infrastructure --startup-project HTask.API
    
    # 2. Aplicar la migración y crear el archivo TaskManager.db
    dotnet ef database update --project HTask.Infrastructure --startup-project HTask.API
    
4.  *Ejecutar el Proyecto API:*
    bash
    dotnet run --project HTask.API
    
5.  *Acceder a Swagger:*
    * El navegador se abrirá automáticamente a la ruta: https://localhost:[PORT]/swagger.

### C. Endpoints Implementados (CRUD)

| Método | Ruta | Descripción |
| :--- | :--- | :--- |
| POST | /api/tasks | Crea una nueva tarea (incluye el estado inicial). |
| GET | /api/tasks | Obtiene todas las tareas. |
| GET | /api/tasks/{id} | Obtiene una tarea específica. |
| PUT | /api/tasks/{id} | Actualiza una tarea existente. |
| DELETE | /api/tasks/{id} | Elimina una tarea por ID. |

---

## 3. ☁️ Azure DevOps (CI/CD)

El repositorio incluye un archivo YAML configurado para automatizar el ciclo de vida del software, cumpliendo con los puntos 3, 4 y 5 de las instrucciones de entrega.

1.  *Integración Continua (CI):* El pipeline se dispara en cada push a main y compila el código, generando un artefacto de publicación.
2.  *Despliegue Continuo (CD):* El pipeline despliega el artefacto compilado al App Service, incluyendo el archivo *TaskManager.db* para que la API sea funcional inmediatamente.

*URL de la Documentación (Swagger/Scalar) del ambiente desplegado:*
https://localhost:7099/swagger/index.html

[*LINK DEL SWAGGER DEL AZURE APP SERVICE DESPLEGADO*]


*Desarrollador Héctor Isaías Trujillo Galicia.
