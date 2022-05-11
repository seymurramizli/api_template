# Template for building microservices
This project contains a shared library, a .NET Core 6 web api project and an sql server database project.
Common library is a shared library between services. It contains infrastructure logic (logging, error handling, filters like validation, mapping).
Service projects will add common library as a reference and won't have to implement infrastructure logic, only business related codes will go in services.
For every service database an sql server database project will be created.
## Technologies
Technologies used - .NET Core 6, EF Core 6, AutoMapper, FluentValidation, LazyCache, Serilog
## Goals
Monorepo, trunk based development, database per service, event sourcing, distributed tracing, exception tracking, unit testing
