using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventureWorks.Core.Tests.SimpleMode
{
    /// <summary>
    /// Simple Mode is when you use directly (without inheriting or defining your own classes) the following classes:
    /// ReadOnlyDbConnection, ReadWriteDbConnection, ReadOnlyRepository{TEntity} and ReadWriteRepository{TEntity}.
    /// It's possible to mock those connections/queries/commands to some degree by using Dapper.Moq.
    /// </summary>
    public class SimpleModeTests
    {
        string cnStr = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");


        /// <summary>
        /// Querying real database using ReadWriteDbConnection directly
        /// </summary>
        [Test]
        public void QueryAll()
        {
            var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(cnStr));
            var allPeople = conn.Query<Person>("SELECT * FROM [Person].[Person]");
            foreach (var person in allPeople.Take(20))
                System.Diagnostics.Debug.WriteLine($"{person.FirstName} {person.LastName}");
        }

        /// <summary>
        /// Generic Repository provides us CRUD helpers (Insert/Update) using Dapper FastCRUD
        /// </summary>
        [Test]
        public void GenericRepo_UpdateSingle()
        {
            var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(cnStr));
            var repo = conn.GetReadWriteRepository<Person>();
            var p1 = conn.QuerySingle<Person>("SELECT TOP 1 * FROM [Person].[Person]");
            p1.LastName = p1.LastName;
            p1.FirstName = p1.FirstName + " lala";
            repo.Update(p1);
            p1 = repo.QueryBuilder().Where($"{nameof(Person.BusinessEntityId):raw}={p1.BusinessEntityId}").QuerySingle();
            Assert.That(p1.FirstName.EndsWith(" lala"));
            p1.FirstName = p1.FirstName.Replace(" lala", "");
            repo.Update(p1);
        }

        /// <summary>
        /// Generic Repository provides us CRUD helpers (Insert/Update) using Dapper FastCRUD
        /// </summary>
        [Test, Order(1)]
        public void GenericRepo_InsertSingle()
        {
            var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(cnStr));

            // Repos are built over the same connection (they share same underlying connection)
            var repo1 = conn.GetReadWriteRepository<BusinessEntity>();
            var repo2 = conn.GetReadWriteRepository<Person>();

            var p1 = new BusinessEntity() { ModifiedDate = DateTime.Now, Rowguid = Guid.NewGuid() }; // unfortunately FastCRUD will specify all columns, so can't send C# DateTime.MinValue to SQL 

            using (var trans = conn.BeginTransaction())
            {
                // Dapper FastCRUD will update back the inserted property with database-generated values
                repo1.Insert(p1, trans);

                var p2 = new Person() {
                    BusinessEntityId = p1.BusinessEntityId,
                    PersonType = "GC",
                    FirstName = "Rick",
                    LastName = "Drizin",
                    ModifiedDate = DateTime.Now, 
                    Rowguid = Guid.NewGuid()
                };
                repo2.Insert(p2, trans);

                trans.Commit();
            }
        }


        /// <summary>
        /// Generic Repository provides us CRUD helpers (Insert/Update) using Dapper FastCRUD
        /// </summary>
        [Test, Order(2)]
        public void GenericRepo_DeleteSingle()
        {
            var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(cnStr));

            // Repos are built over the same connection (they share same underlying connection)
            var repo1 = conn.GetReadWriteRepository<BusinessEntity>();
            var repo2 = conn.GetReadWriteRepository<Person>();

            var p2 = repo2.QueryAll().ToList().Where(p => p.FirstName == "Rick" && p.LastName == "Drizin").First();
            var p1 = repo1.QueryAll().ToList().Where(be => be.BusinessEntityId == p2.BusinessEntityId).Single();

            using (var trans = conn.BeginTransaction())
            {
                // Dapper FastCRUD will update back the inserted property with database-generated values
                repo2.Delete(p2, trans);
                repo1.Delete(p1, trans);

                trans.Commit();
            }
        }

    }
}
