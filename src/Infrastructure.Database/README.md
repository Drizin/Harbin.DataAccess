# Description

This is Data Access library based on [Dapper](https://github.com/StackExchange/Dapper), [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/), and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder).
It implement Repositories (Generic Repository Pattern) and helps to manage connections to distributed databases and/or read-replicas.

#  Design Principles

Harbin Database Library was designed based on the following ideas:
- Wrappers around IDbConnection, but which also implement IDbConnection so can be used as regular connections.
- "Bare metal", does not try to "hide" ADO.NET or Dapper, so you can use the full power of Dapper, IDbTransactions, etc.
- Easy to manage multiple database connections, either to different databases (e.g. distributed databases, microservices, heterogeneous databases) or to differentiate masters vs read-only replicas.
- ReadOnlyConnection, ReadOnlyConnection<DB>, ReadWriteConnection, ReadWriteConnection<DB>.
- Those classes respectively can build ReadOnlyRepository<TEntity> or ReadWriteRepository<TEntity> which are Generic Repositories (Generic Repository Patter) for your Entities.
- ReadOnlyRepository<TEntity> includes facades to Dapper Query Methods, and also facades to DapperQueryBuilder methods.
- ReadWriteRepository<TEntity> includes facades to Dapper FastCRUD methods so you can easily get INSERT/UPDATE/DELETE as long as you decorate your entities with attributes like **[Key]** and **[DatabaseGenerated(DatabaseGeneratedOption.Identity)]** .
- Repositories (ReadOnlyRepository / ReadWriteRepository) and Connections (ReadOnlyConnection / ReadWriteConnection) can be extended either through method extensions or through inheritance.
- By keeping Queries on ReadOnlyRepository and DbCommands on ReadWriteRepository you're isolating your Queries and Commands (CQRS).
- You can unit test your application even if it depends on ReadOnlyConnection, ReadWriteConnection, ReadOnlyRepository, ReadWriteRepository, etc. They all can be "faked" using inheritance or a mocking library.


## License
MIT License
