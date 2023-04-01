﻿using AdventureWorks.Core.Domain.Entities;
using AdventureWorks.Core.Tests.CustomClassesReadWrite;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    /// <summary>
    /// CustomClasses are the most configurable way of adding custom behavior to your connections or repositories:
    /// - By inheriting ReadWriteDbConnection you can add new DbCommands
    /// - By inheriting ReadDbConnection/ReadOnlyDbConnection/ReadWriteDbConnection you can return custom Repositories 
    /// - By defining custom Repositories (inheriting from ReadDbRepository{TEntity}/ReadOnlyDbRepository{TEntity}/ReadWriteDbRepository{TEntity})
    /// you can add new Queries (or even Query objects)
    /// </summary>
    public class CustomClassesTests
    {
        string cnStr = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");

        [TearDown]
        public void TearDown()
        {
            DefaultFactories.ReadWriteDbRepositoryFactory = new ReadWriteDbRepositoryFactory();
            DefaultFactories.ReadDbRepositoryFactory = new ReadDbRepositoryFactory();
            DefaultFactories.ReadDbRepositoryFactory = new ReadDbRepositoryFactory();
        }


        /// <summary>
        /// Service receive MyDBConnection (which is a ReadWriteDbConnection) 
        /// which offers a custom PersonRepository inheriting from ReadWriteDbRepository{Person}
        /// </summary>
        [Test]
        public void TestRealDatabase()
        {
            var conn = new MyDBConnection(new System.Data.SqlClient.SqlConnection(cnStr));
            DefaultFactories.ReadWriteDbRepositoryFactory.RegisterTransient<Person, PersonRepository>(conn => new PersonRepository(conn));

            var svc = new MyAppService(conn);
            var bestCustomers = svc.GetUpdatedBestCustomers();
            Assert.That(bestCustomers.Count() >= 10);
        }

        /// <summary>
        /// FakeMyDBConnection is fully fake, the service will receive a FakePersonRepository which returns only a fixed person record.
        /// </summary>
        [Test]
        public void TestManuallyMockedRepository()
        {
            var conn = new FakeMyDBConnection();
            DefaultFactories.ReadWriteDbRepositoryFactory.RegisterTransient<Person, PersonRepository>(conn => new FakePersonRepository(conn));

            var svc = new MyAppService(conn);
            var bestCustomers = svc.GetUpdatedBestCustomers();
            Assert.That(bestCustomers.Count() == 1);
        }


        [Test]
        public void TestMoqqedRepository()
        {
            Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
            var mockRepo = new Mock<PersonRepository>();
            mockRepo.Setup(repo => repo.GetBestCustomers()).Returns(new List<Person>() { p1 });

            var mockConn = new Mock<MyDBConnection>();
            //mockConn.Setup(conn => conn.CalculateBestCustomers()); // no need to mock anything since it's a void.. will just be ignored
            mockConn.Setup(conn => conn.GetReadWriteRepository<Person>()).Returns((IReadWriteDbRepository<Person>)mockRepo.Object);

            var svc = new MyAppService(mockConn.Object);
            var bestCustomers = svc.GetUpdatedBestCustomers();
            Assert.That(bestCustomers.Count() == 1);
        }

    }
}
