# Description

Harbin is a .NET Layered Architecture with support for distributed databases (read replicas) - using Dapper, Generic Repositories (Extensible and Mockable), and Code Generation (CodegenCS).  

It contains some reusable components (e.g. Data Access) and some Sample Projects to show how Harbin Architecture works.

Harbin reusable components (libraries):

Project | Description
------------ | -------------
[**Harbin.DataAccess**](https://github.com/Drizin/Harbin/tree/master/src/Harbin.DataAccess) [(documentation)](https://github.com/Drizin/Harbin/tree/master/src/Harbin.DataAccess) | Data Access library (based on [Dapper](https://github.com/StackExchange/Dapper), [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/), and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder)) which implement Repositories (Generic Repository Pattern) and can be used over distributed databases and/or read-replicas.
Harbin.Infrastructure | Infrastructure Services: wrappers around Redis, RabbitMQ, MediatR, ElasticSearch, Serilog, Mail/File utilities

Harbin sample project(s) - based on Microsoft AdventureWorks database:

Project | Description
------------ | -------------
[**AdventureWorks.Core.Domain**](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Domain) | Domain Layer (only Entities) 
[**AdventureWorks.Core.CoreDatabase**](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.CoreDatabase) | Data Layer (aka Persistence Layer) - where we define connections, custom Queries and DbCommands. Also includes [CodegenCS](https://github.com/Drizin/CodegenCS/) scripts to extract the database schema and generate Entities in Domain Layer.
[AdventureWorks.Core.Application](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Application) | Application Layer (Services)
[AdventureWorks.Core.Tests](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Tests) | Unit Tests which test the Application Layer (Services) but mocking Database Connection or Repositories


#  Design Principles

Harbin architecture is mostly based on traditional 3-tiered architecture (mostly for being pragmatic and keeping things simple for data-driven applications), but borrows some ideas from Domain-Driven Design (and Onion Architecture).

It was designed based on the following ideas (from lowest layer to top):
- Like DDD/Onion, Domain Model (mostly Entities, since this is still a data-driven architecture) is the lowest layer (doesn't depend on any other layer) and can contain business logic, but no data access.
- Like DDD/Onion, business logic which doesn't fit in the Domain Model should be in upper layers. But we avoid distinction between "Domain Services" and "Application Services" because it's kind of gray area.
- The Data Layer contains Repositories and CRUD, and it's a lower layer which depend only on Domain Entities. So it doesn't implement any interface defined in upper layers or elsewhere.
- Since the Data Layer doesn't use interfaces we avoid code repetition and upper layers can use Data Layer without Dependency Injection or Dependency Inversion. Like traditional 3-tier architecture.
- Application Layer (Services) can use Domain Model but can also use Data Layer, so it can access data stores.
- Data Layer uses a [Database Library](https://github.com/Drizin/Harbin/tree/master/src/Harbin.DataAccess)lk which offers "bare metal" access to Dapper/ADO.NET but also helps to manage multiple database connections (e.g. distributed databases, microservices, heterogeneous databases) or to differentiate masters vs read-only replicas.
- This Database Library makes it easy to manage multiple database connections, including connections to read-only replicas, and separation between Queries (read-only) and DbCommands (read-write), as proposed by CQRS.
- This Database Library implements Generic Repository Pattern (using libraries [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/) and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder)) to make your CRUD as easy as possible - you just have to add attributes to your Domain Entities.
- Even though Data Layer is not based on interfaces it's still fully mockable - any method can be mocked using inheritance, without the hassle of having to write interfaces/classes.
- Data Layer includes [CodegenCS (Code Generator)](https://github.com/Drizin/CodegenCS/) scripts to automatically extract the database schema and generate POCO classes. Easy to plug your own database.


## About the name
Harbin is the capital of Heilongjiang (China’s northernmost province) and one of the largest cities in China.  
The city grew in the late 19th century with the influx of Russian engineers constructing the eastern leg of the Trans-Siberian Railroad.  
This is just a minor tribute to my father who was born in Harbin (as part of a family of Russian immigrants). He passed away in 2019.

## License
MIT License
