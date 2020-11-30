# Description

Data Access library based on [Dapper](https://github.com/StackExchange/Dapper), [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/), and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder).  
Implements Generic Repositories (Generic Repository Pattern) but yet allows us to extend those Repositories with custom Queries and Commands.  
Helps to manage connections to distributed databases and/or read-replicas.  

#  Design Principles

Harbin Database Library was designed based on the following ideas:
- This is a "Bare metal" library - it does not try to "hide" or abstract ADO.NET or Dapper, so you can use the full power of Dapper, IDbTransactions, etc.
- There are classes for read-only connections (`ReadOnlyDbConnection`, `ReadOnlyDbConnection<DB>`) and for read-write connections (`ReadWriteDbConnection`, `ReadWriteDbConnection<DB>`).
- By isolating read-only connections and read-write connections we can use read-replicas for scaling-out reads on large scale apps.
- All connection classes are wrappers around IDbConnection but which also implement IDbConnection so they can be used as regular connections.
- If your app uses multiple databases (e.g. distributed databases, microservices, heterogeneous databases) it's easy to manage those multiple connections using generic classes `ReadOnlyDbConnection<DB>` and `ReadWriteDbConnection<DB>`.
- Read connections can build `ReadRepository<TEntity>` and read-write connections can build `ReadWriteRepository<TEntity>` - those are Generic Repositories (Generic Repository Pattern).
- `ReadRepository<TEntity>` has methods to invoke Dapper Query extensions (Query, QuerySingle, QueryAsync, etc).
- `ReadRepository<TEntity>` has methods to invoke DapperQueryBuilder methods, so you can easily add dynamic filters to your queries.
- `ReadWriteRepository<TEntity>` has **Insert** / **Update** / **Delete** methods which use Dapper FastCRUD to automatically generate the CRUD as long as your entities are decorated with attributes like **[Key]** and **[DatabaseGenerated(DatabaseGeneratedOption.Identity)]** .
- By isolating read-only repositories and read-write repositories we can **isolate Queries and Commands (CQRS)** - Queries should be added to `ReadRepository` and DbCommands to `ReadWriteRepository`.
- Repositories are "persistence-based repositories" (as described by Vaughn Vernon in "Implementing Domain-Driven Design" book), and do NOT emulate a collection of objects (as described by Eric Evans in "Domain-Driven Design" book).
- Repositories and Connections can be extended either through inheritance (recommended) or through method extensions.
- It's possible to register a custom repository type (e.g. `PersonRepository` child of `ReadWriteRepository<Person>`) to be used instead of the generic repository.
- Your Application Logic/Services can be tested by registering fake repository implementations or by using a mocking library.
- Everything is testable: connection wrappers and their underlying connections are also mockable.

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

**Extending Repositories (adding custom Queries and Commands) using Inheritance (recommended approach)**
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

In the example above the `ReadWriteRepository<Person>` is replaced by a derived type `PersonRepository`, and the cast expects that type.  
It would be possible to use interfaces (e.g. `IPersonRepository`, child of `IReadWriteRepository<Person>`) but in order to make simple examples (avoid repetitive code) we're not using interfaces here.


**Extending Repositories (adding custom Queries and Commands) using Extension Methods (alternative approach)**

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

PS: This alternative method (using Extension methods) is easier than using Inheritance (because it doesn't require Registering Types and casting), but it's not recommended because it has limited testing support:
- Even if you create a fake implementation of the repository you can't override static extension methods with fake methods
- However, you can still use IDbConnection stubs (see Moq.Dapper below) to return fake results to your Dapper calls.


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


**Mocking your repositories using a fake class**

In a previous example the `ReadWriteRepository<Person>` was replaced by a derived type `PersonRepository`, and the cast expects that type (without using interfaces to make the example simple and avoid repetitive code).  
Since we're not using interfaces (we're casting/expecting `PersonRepository`), the easiest way of creating a fake repository is using inheritance (`FakePersonRepository` derived from `PersonRepository`), and overriding only what you need.  

```cs
public class FakePersonRepository : PersonRepository
{
  public FakePersonRepository() : base((IReadWriteDbConnection)null)
  {
  }
  public override IEnumerable<Person> QueryRecentEmployees()
  {
    Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
    return new List<Person>() { p1 };
  }
  public override void UpdateCustomers()
  {
    // do nothing (avoids crashing on the null underlying connection)
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

PS: You can't override static extension methods 

**Mocking your repositories using stubs created by a Mocking library (Moq)**
```cs
public void SampleTest()
{
  
  // Mock PersonRepository with a fake implementation of QueryRecentEmployees()
  Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
  var mockRepo = new Mock<PersonRepository>(); // or IPersonRepository if using interfaces
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

PS: Moq library can't create stubs for static extension methods 

**Mocking IDbConnection and Dapper calls with Moq and [Moq.Dapper](https://github.com/UnoSD/Moq.Dapper)**

```cs
var connection = new Mock<IDbConnection>();
Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
connection.SetupDapper(c => c.Query<Person>(It.IsAny<string>(), null, null, true, null, null)).Returns(new List<Person>() { p1 });

var conn = new ReadWriteDbConnection(connection.Object);
var repo1 = conn.GetReadWriteRepository<Person>();
repo1.QueryAll();
UpdateCustomers
repo1.UpdateCustomers(); // Mock by default will just return void for methods which aren't explicily mocked
var recentEmployees = repo1.QueryRecentEmployees();
```

Even if `QueryRecentEmployees()` is an extension method it will still be mocked by the underlying stubs which replace IDbConnection and which return fake results for Dapper calls.


**Managing different connections for Read-Write datastore and for Read Replicas**

You can register `ReadWriteDbConnection` (in your IoC container) with a fixed connection string pointing to the master database (read-write store).  
You can register `ReadOnlyDbConnection` (in your IoC container) with `DbConnectionFactory` which randomized against some different read-replica stores.

**Managing different databases (distributed databases)**

You can register and use different connections to multiple databases by using the `ReadDbConnection<DB>` and `ReadWriteDbConnection<DB>` generic classes.  
Example: `ReadWriteDbConnection<CustomersDB>`, `ReadWriteDbConnection<OrdersDB>`, etc.


## License
MIT License
