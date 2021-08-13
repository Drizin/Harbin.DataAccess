using AdventureWorks.Core.Domain.Entities;
using Dapper;
using Harbin.DataAccess.DapperFastCRUD.Connections;
using Harbin.DataAccess.DapperFastCRUD.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    /// <summary>
    /// CustomClasses are the most configurable way of adding custom behavior to your connections or repositories:
    /// - By inheriting ReadWriteDbConnection you can add new DbCommands
    /// - By inheriting ReadDbConnection/ReadOnlyDbConnection/ReadWriteDbConnection you can return custom Repositories 
    /// - By defining custom Repositories (inheriting from ReadDbRepository{TEntity}/ReadOnlyDbRepository{TEntity}/ReadWriteDbRepository{TEntity})
    /// you can add new Queries (or even Query objects)
    /// </summary>
    public class CustomClassesReadWriteTests
    {
        string cnStr = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");

        [TearDown]
        public void TearDown()
        {
            MyDBReadConnection.CleanRegisteredRepositories();
            MyDBReadWriteConnection.CleanRegisteredRepositories();
        }
        


        /// <summary>
        /// Service receive MyDBConnection (which is a ReadWriteDbConnection) 
        /// which offers a custom PersonRepository inheriting from ReadWriteDbRepository{Person}
        /// </summary>
        [Test]
        public void TestRealDatabase()
        {
            var rconn = new MyDBReadConnection(new System.Data.SqlClient.SqlConnection(cnStr));
            var wconn = new MyDBReadWriteConnection(new System.Data.SqlClient.SqlConnection(cnStr));

            MyDBReadConnection.RegisterRepositoryType<Person, PersonReadRepository>();
            MyDBReadWriteConnection.RegisterRepositoryType<Person, PersonReadWriteRepository>();

            var svc = new MyAppService(rconn, wconn);
            var bestCustomers = svc.GetUpdatedBestCustomers(); // crashes on this cast: var repo1 = (PersonReadRepository) _rconn.GetReadRepository<Person>();
            Assert.That(bestCustomers.Count() >= 10);
        }


        [Test]
        public void TestMoqqedRepository()
        {
            Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
            var mockRepo1 = new Mock<PersonReadRepository>();
            mockRepo1.Setup(repo => repo.GetBestCustomers()).Returns(new List<Person>() { p1 });

            var mockRepo2 = new Mock<PersonReadWriteRepository>();
            //mockRepo2.Setup(repo => repo.CalculateBestCustomers()); // no need to mock anything since it's a void.. will just be ignored

            var r_mockConn = new Mock<MyDBReadConnection>();
            var w_mockConn = new Mock<MyDBReadWriteConnection>();
            r_mockConn.Setup(conn => conn.GetReadRepository<Person>()).Returns((IReadDbRepository<Person>)mockRepo1.Object);
            w_mockConn.Setup(conn => conn.GetReadWriteRepository<Person>()).Returns((IReadWriteDbRepository<Person>)mockRepo2.Object);

            var svc = new MyAppService(r_mockConn.Object, w_mockConn.Object);
            var bestCustomers = svc.GetUpdatedBestCustomers();
            Assert.That(bestCustomers.Count() == 1);
        }

    }
}
