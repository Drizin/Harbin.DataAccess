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

## Installation
Just install nuget package **[Harbin.Infrastructure.Database](https://www.nuget.org/packages/Harbin.Infrastructure.Database/)**, 
add `using Harbin.Infrastructure.Database.Connection`, `using using Harbin.Infrastructure.Database.Repositories`, and start using (see examples below).  
See documentation below, or more examples in [unit tests](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Tests).

## Documentation

**Define your classes with attributes to describe Keys and Identity columns**

```cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ContactType", Schema = "Person")]
public partial class ContactType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContactTypeId { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string Name { get; set; }

}
```

**Basic usage: Creating a connection, Querying, Inserting and Updating**

```cs
// This is basically a wrapper around your own IDbConnection 
// (which can be any database supported by Dapper FastCRUD: LocalDb, Ms Sql Server, MySql, SqLite, PostgreSql)
// You can use ReadDbConnection or ReadWriteDbConnection which is a derived class
var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(connectionString));

// ReadDbConnection (and derived ReadWriteDbConnection) has Dapper facades for all Dapper Query methods 
// (including Async methods), no need to add "using Dapper".
var contactTypes = conn.Query<ContactType>("SELECT * FROM Person.ContactType");

// Since we have a ReadWriteDbConnection we can get a Generic ReadWriteRepository for 
// our Entities (IReadWriteRepository<TEntity>), which offers Update/Insert/Delete methods:

// Updating a record using Generic Repository Pattern (using FastCRUD):
var contactType = contactTypes.First();
contactType.ModifiedDate = DateTime.Now;
conn.GetReadWriteRepository<ContactType>().Update(contactType);

// Adding a new record using Generic Repository Pattern (using FastCRUD):
var newContactType = new ContactType() { Name = "NewType", ModifiedDate = DateTime.Now };
conn.GetReadWriteRepository<ContactType>().Insert(newContactType);

// Deleting using Generic Repository Pattern (using FastCRUD):
conn.GetReadWriteRepository<ContactType>().Delete(newContactType);

// Both with ReadWriteDbConnection or ReadDbConnection we can get a IReadRepository<TEntity> 
// which has some helpers to Query our table:
var all = conn.GetReadRepository<ContactType>().QueryAll();
all = conn.GetReadRepository<ContactType>().Query("SELECT * FROM Person.ContactType WHERE ContactTypeId < 5");
all = conn.GetReadRepository<ContactType>().Query("WHERE ContactTypeId < 5"); // just syntactic sugar to automatically fill the table/schema according to class attributes
all = conn.GetReadRepository<ContactType>().Query("ContactTypeId < 5"); // more syntactic sugar

// DapperQueryBuilder allows to dynamically append new conditions and is also safe against sql-injection 
// (parameters can be described using string interpolation and it's converted into Dapper DynamicParams)
var dynamicQuery = conn.GetReadRepository<ContactType>().QueryBuilder();
dynamicQuery.Where($"ContactTypeId < 5");
dynamicQuery.Where($"ModifiedDate < GETDATE()");
string search = "%Sales%";
dynamicQuery.Where($"Name LIKE {search}");
all = dynamicQuery.Query();
```

**Adding reusable Queries and Commands using Extension Methods**
...

**Adding reusable Queries and Commands using inheritance**
...



## License
MIT License
