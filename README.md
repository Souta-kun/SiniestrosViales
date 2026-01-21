# Prueba técnica para desarrollador backend .NET senior

## Descripcion

Proyecto API Rest, desarrollado en .Net 8, el cual presenta 2 endpoints, uno de creacion y otro de consulta de siniestros viales.

- Arquitectura implementada: Clean Arquitecture
- Principios: SOLID, DDD, DRY, KISS, YAGNI
- Patrones: Mediator, CQRS

## Instalacion del Proyecto

Prerrequisitos:

- DotNet 8
- Docker
- Docker compose

Se deben seguir los siguientes pasos para la ejecucion del proyecto:

1. Clonar el repositorio
2. En la raíz del proyecto (donde está docker-compose.yml), correr el comando:
   > docker compose up -d
3. Restaurar dependencias con:
   > dotnet restore
4. Ejecutar las siguientes migraciones:
   > dotnet ef database update -s SiniestrosViales.Api -p SiniestrosViales.Infrastructure
5. Ejecutar el proyecto con:
   > dotnet run --project SiniestrosViales.Api
6. El proyecto deberia desplegarse en la ruta:
   > http://localhost:5007/swagger/index.html

## Explicacion decisiones arquitectonicas

El proyecto se desarrolló usando un estilo arquitectónico como Clean Architecture, con una arquitectura basada en microservicios, la cual gestiona el dominio de "Siniestros", encargándose así de la creación y consulta de los mismos.

Se crearon 4 proyectos:

SiniestrosViales.Api: Punto de entrada de las peticiones http
SiniestrosViales.Application: Encapsula la lógica del negocio
SiniestrosViales.Domain: Contiene la definición del dominio
SiniestrosViales.Infrastructure: En esta capa se da la persistencia de los datos

Se concibe como un microservicio, porque permite tener la responsabilidad única de gestionar el siniestro, con una base de datos independiente y una arquitectura escalable. Se usó clean architecture, porque se enfoca en el dominio y lo aísla de capas como la infraestructura; permitiendo cambiarla sin afectar el mismo dominio, permitiendo escalar el proyecto a demanda.

Para la persistencia de los datos se usó Entity Framework Core, un ORM que es capaz de dar gestión completa de los datos, a través de sus diferentes funciones; facilitando la persistencia y dando robustez a la implementación.
Se usó CodeFirst, es decir se desarrolló el modelo de siniestros y después se creó la tabla en base de datos por medio de migraciones.

El proyecto implementa principios SOLID, como SRP; en donde a través de CQRS delegamos responsabilidades únicas de lectura y escritura al dominio de Siniestros. Por otro lado, hacemos uso del patrón repository y DIP, en donde centralizamos la persistencia de los datos a través de repositorios y a su vez, no dependemos en el proyecto de implementaciones, si no de abstracciones; esto lo vemos en el uso de interfaces y no de las implementaciones.

Se implementó un Middleware, para capturar todos los posibles errores y dar una respuesta general al usuario. De la misma manera, implementamos una clase que nos permitiera dar una respuesta con el mismo formato, abarcando si la petición es exitosa, un posible mensaje, y la posible data de respuesta.

Finalmente, implementamos un health check, para validar el estado de la base de datos; este lo encontramos en la ruta: http://localhost:5007/health

