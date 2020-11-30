# Description

This is Data Access library based on [Dapper](https://github.com/StackExchange/Dapper), [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/), and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder).
It implement Repositories (Generic Repository Pattern) and helps to manage connections to distributed databases and/or read-replicas.

#  Design Principles

Harbin Database Library was designed based on the following ideas:
- Wrappers around IDbConnection, but which also implement IDbConnection so can be used as regular connections.
- "Bare metal", does not try to "hide" ADO.NET or Dapper, so you can use the full power of Dapper, IDbTransactions, etc.
- Easy to manage multiple database connections, either to different databases (e.g. distributed databases, microservices, heterogeneous databases) or to differentiate masters vs read-only replicas.
- ReadOnlyDbConnection, ReadOnlyDbConnection<DB>, ReadWriteDbConnection, ReadWriteDbConnection<DB>.
- Those classes respectively can build ReadRepository<TEntity> or ReadWriteRepository<TEntity> which are Generic Repositories (Generic Repository Patter) for your Entities.
- ReadRepository<TEntity> includes facades to Dapper Query Methods, and also facades to DapperQueryBuilder methods.
- ReadWriteRepository<TEntity> includes facades to Dapper FastCRUD methods so you can easily get INSERT/UPDATE/DELETE as long as you decorate your entities with attributes like **[Key]** and **[DatabaseGenerated(DatabaseGeneratedOption.Identity)]** .
- Repositories (ReadRepository / ReadWriteRepository) and Connections (ReadConnection / ReadWriteDbConnection) can be extended either through method extensions or through inheritance.
- By keeping Queries on ReadRepository and DbCommands on ReadWriteRepository you're isolating your Queries and Commands (CQRS).
- You can unit test your application even if it depends on ReadConnection, ReadWriteDbConnection, ReadRepository, ReadWriteRepository, etc. They all can be "faked" using inheritance or a mocking library.

## Installation
Just install nuget package **[Harbin.Infrastructure.Database](https://www.nuget.org/packages/Harbin.Infrastructure.Database/)**, 
add `using Harbin.Infrastructure.Database.Connection`, `using using Harbin.Infrastructure.Database.Repositories`, and start using (see examples below).  
See documentation below, or more examples in [unit tests](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Tests).

## Documentation

**Define your Entities and decorate attributes on Primary Keys and Identity columns**

Generic Repositories by default use Dapper FastCRUD so you have to describe your entities accordingly.

```cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// AdventureWorks is a sample database which uses schemas for grouping tables
// If table uses default schema and table name matches class name you don't need the [Table] attribute
[Table("ContactType", Schema = "Person")]
public partial class ContactType
{
    [Key] // column is part of primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // column is auto-increment
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
```cs
public static class PersonQueryExtensions
{
  public static IEnumerable<Person> QueryRecentEmployees(this IReadDbRepository<Person> repo)
  {
    return repo.Query("SELECT TOP 10 * FROM [Person].[Person] WHERE [PersonType]='EM' ORDER BY [ModifiedDate] DESC");
  }
}

public static class PersonDbCommandExtensions
{
  public static void UpdateCustomers(this IReadWriteDbRepository<Person> repo)
  {
    repo.Execute("UPDATE [Person].[Person] SET [FirstName]='Rick' WHERE [PersonType]='EM' ");
  }
}
public void Sample()
{
  var repo = conn.GetReadWriteRepository<Person>();
  
  // Generic Repository methods
  repo.Insert(new Person() { FirstName = "Rick", LastName = "Drizin" });
  var allPeople = repo1.QueryAll();
  
  // Custom Extensions
  repo.UpdateCustomers();
  var recentEmployees = repo.QueryRecentEmployees();
}
```

**Adding reusable Queries and Commands using inheritance**
```cs
public class PersonRepository : ReadWriteDbRepository<Person>
{
  public PersonRepository(IReadWriteDbConnection db) : base(db)
  {
  }
  public virtual IEnumerable<Person> QueryRecentEmployees()
  {
    return this.Query("SELECT TOP 10 * FROM [Person].[Person] WHERE [PersonType]='EM' ORDER BY [ModifiedDate] DESC");
  }
  public virtual void UpdateCustomers()
  {
    this.Execute("UPDATE [Person].[Person] SET [FirstName]='Rick' WHERE [PersonType]='EM' ");
  }
}
public void Sample()
{
  // Registers that GetReadWriteRepository<Person>() should return a derived type PersonRepository
  ReadWriteDbConnection.RegisterRepositoryType<Person, PersonRepository>();

  var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(connectionString));  
  
  // we know exactly what subtype to expect
  var repo = (PersonRepository) conn.GetReadWriteRepository<Person>();
  
  repo.UpdateCustomers();
  var recentEmployees = repo.QueryRecentEmployees();
}

```

**Running multiple commands under a single ADO.NET transaction**
```cs
public static class PersonDbCommandExtensions
  public static void UpdateCustomers(this IReadWriteDbRepository<Person> repo, IDbTransaction transaction = null, int? commandTimeout = null)
  {
    repo.Execute("UPDATE [Person].[Person] SET [FirstName]=@firstName WHERE [PersonType]='EM' ", 
      param: new { firstName = "Rick" }, transaction: transaction, commandTimeout: commandTimeout);
  }
  public static void OtherCommand(this IReadWriteDbRepository<Person> repo, IDbTransaction transaction = null, int? commandTimeout = null)
  {
    repo.Execute("other command...", param: new { etc }, transaction: transaction, commandTimeout: commandTimeout);
  }
}
public void SampleTransaction()
{
  var repo = conn.GetReadWriteRepository<Person>();

  using (var trans = conn.BeginTransaction())
  {
    repo.UpdateCustomers(trans, commandTimeout: 30);
    repo.QueryRecentEmployees(trans);
    trans.Commit();
  }
}
```

**Managing different connections for Read-Write datastore and for Read Replicas**

... ReadDbConnection vs ReadWriteDbConnection vs ReadOnlyDbConnection; ReadRepository vs ReadWriteRepository;

**Managing different databases (distributed databases)**

... ReadDbConnection<DB>, ReadWriteDbConnection<DB>


**Mocking your repositories using a fake class**

```cs
public class FakePersonRepository : PersonRepository
{
  public FakePersonRepository(IReadWriteDbConnection db) : base(db)
  {
  }
  public override IEnumerable<Person> QueryRecentEmployees()
  {
    Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
    return new List<Person>() { p1 };
  }
  public override void UpdateCustomers()
  {
    // do nothing
  }
}
public void SampleTest()
{
  // Registers that GetReadWriteRepository<Person>() should return a derived type FakePersonRepository
  ReadWriteDbConnection.RegisterRepositoryType<Person, FakePersonRepository>();

  var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(connectionString));  
  
  // your business logic (and services) could still expect PersonRepository,
  // but they will get a fake implementation
  var repo = (PersonRepository) conn.GetReadWriteRepository<Person>();
  
  repo.UpdateCustomers();
  var recentEmployees = repo.QueryRecentEmployees();
}
```

**Mocking your repositories using a Mocking library (Moq)**
```cs
public void SampleTest()
{
  
  // Mock PersonRepository with a fake implementation of QueryRecentEmployees()
  Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
  var mockRepo = new Mock<PersonRepository>();
  mockRepo.Setup(repo => repo.QueryRecentEmployees()).Returns(new List<Person>() { p1 });
  //mockRepo.Setup(repo => repo.UpdateCustomers()); // no need to mock anything since it's a void.. will just be ignored

  // Mock ReadWriteDbConnection so when some service asks for IReadWriteRepository<Person>
  // it will get the mocked repository
  var mockConn = new Mock<ReadWriteDbConnection>();
  mockConn.Setup(conn => conn.GetReadWriteRepository<Person>()).Returns((IReadWriteDbRepository<Person>)mockRepo.Object);

  // Instead of injecting real connections into your services you should 
  // provide the mocked connection which will provide a mocked repository
  var svc = new MyAppService(mockConn.Object);
  var recentEmployees = svc.QueryRecentEmployees();
  Assert.That(recentEmployees.Count() == 1);
}
```

## License
MIT License
