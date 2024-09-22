Warehouse360
|── docs: solution folder
|   └── Right now empty folder
|
|── src: solution folder
|   ├── Warehouse360.Core: Project Type class library
|   │   ├── SeedWork
|   │   │   ├── Entities
|   |   |   |   └── BaseEntity.cs
|   │   │   ├── Interfaces
|   │   │   │   ├── IAggregateRoot.cs
|   │   │   │   ├── IRepository.cs
|   │   │   │   └── IUnitOfWork.cs
|   │   │   └── ValueObjects
|   |   |       └── ValueObject.cs
|   │   ├── InventoryManagement
|   │   │   ├── Aggregates
|   │   │   ├── Entities
|   |   |   ├── Enums
|   |   |   ├── Services
|   │   │   └── ValueObjects
|   │   ├── OrderManagement
|   │   │   ├── Aggregates
|   │   │   ├── Entities
|   |   |   ├── Enums
|   |   |   ├── Services
|   │   │   └── ValueObjects
|   │   └── CustomerManagement
|   │       ├── Aggregates
|   │       ├── Entities
|   |       ├── Enums
|   |       ├── Services
|   │       └── ValueObjects
|   │
|   ├── Warehouse360.Application: Project Type class library
|   |   ├── Command
|   |   ├── Query
|   |   └── Handler
|   │
|   ├── Warehouse360.Infrastructure: solution folder
|   |   ├── Postgres: Project Type class library
|   |   └── Redis: Project Type class library
|
|── tests: solution folder
|   ├── Warehouse360.UnitTests: Project Type test
|   │   └── Application: Folder
|   │       └── Handlers: Folder
|   └── Warehouse360.IntegrationTests: Project Type test