using AdventureWorks.Core.CoreDatabase;
using AdventureWorks.Core.Domain.Entities;
using Dapper;
using Harbin.DataAccess.Connection;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Dapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventureWorks.Core.Tests.ExtensionMethods
{
    /// <summary>
    /// ExtensionMethods are the easiest way of adding custom behavior to your connections or repositories:
    /// - Query extension methods over IReadDbRepository{TEntity}
    /// - DbCommand extension methods over IReadWriteDbConnection
    /// It's possible to mock those connections/queries/commands to some degree by using Dapper.Moq.
    /// </summary>
    public class CustomClassesTests
    {
        string cnStr = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");

        /// <summary>
        /// Queries are defined as extensions over IReadDbRepository{TEntity}
        /// Commands are defined as extensions over IReadWriteDbConnection
        /// </summary>
        [Test]
        public void UseExtensions()
        {
            var conn = new ReadWriteDbConnection(new System.Data.SqlClient.SqlConnection(cnStr));

            var repo1 = conn.GetReadWriteRepository<Person>(); // would also work with ReadOnlyRepository

            conn.CalculateBestCustomers();
            var bestCustomers = repo1.GetBestCustomers();
        }


        /// <summary>
        /// Queries are defined as extensions over IReadDbRepository{TEntity}
        /// Commands are defined as extensions over IReadWriteDbConnection
        /// </summary>
        [Test]
        public void DapperMoq()
        {
            var connection = new Mock<IDbConnection>();
            Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
            connection.SetupDapper(c => c.Query<Person>(It.IsAny<string>(), null, null, true, null, null)).Returns(new List<Person>() { p1 });

            var conn = new ReadWriteDbConnection(connection.Object);

            var repo1 = conn.GetReadWriteRepository<Person>(); // would also work with ReadOnlyRepository
            repo1.QueryAll();

            conn.CalculateBestCustomers(); // Mock by default will just return void for methods which aren't explicily mocked
            var bestCustomers = repo1.GetBestCustomers();

            var wconn = new CoreDbReadWriteConnection(connection.Object);
            var rconn = new CoreDbReadConnection(connection.Object);
            var svc = new AdventureWorks.Core.Application.ApplicationServices.PersonService(rconn, wconn);
            var allPeople = svc.GetAllPeople();
            foreach (var person in allPeople.Take(20))
                System.Diagnostics.Debug.WriteLine($"{person.FirstName} {person.LastName}");
        }



    }
}
