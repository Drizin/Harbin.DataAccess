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

namespace AdventureWorks.Core.Tests.GenericMode
{
    public class Tests
    {
        string cnStr1 = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");
        string cnStr2 = ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection");

        /// <summary>
        /// Querying real database using ReadWriteDbConnection directly
        /// </summary>
        [Test]
        public void Test1_QueryAll()
        {
            var conn1 = new ReadWriteDbConnection<DB1>(new System.Data.SqlClient.SqlConnection(cnStr1));
            var conn2 = new ReadWriteDbConnection<DB2>(new System.Data.SqlClient.SqlConnection(cnStr2));
            var allPeople1 = conn1.Query<Person>("SELECT * FROM [Person].[Person]");
            var allPeople2 = conn2.Query<Person>("SELECT * FROM [Person].[Person]");
            foreach (var person in allPeople1.Take(20))
                System.Diagnostics.Debug.WriteLine($"{person.FirstName} {person.LastName}");
        }

        /// <summary>
        /// Generic ConnectionFactories - can be used to inject connection strings according to the Database
        /// </summary>
        [Test]
        public void Test2_ConnectionFactories()
        {
            IDbConnectionFactory<DB1> factory1 = new Fac1();
            IDbConnectionFactory<DB2> factory2 = new Fac2();

            // The idea is that 
            var conn1 = new ReadWriteDbConnection<DB1>(factory1);
            var conn2 = new ReadWriteDbConnection<DB2>(factory2);

            var allPeople1 = conn1.Query<Person>("SELECT * FROM [Person].[Person]");
            var allPeople2 = conn2.Query<Person>("SELECT * FROM [Person].[Person]");
            foreach (var person in allPeople1.Take(20))
                System.Diagnostics.Debug.WriteLine($"{person.FirstName} {person.LastName}");
        }

        class Fac1 : IDbConnectionFactory<DB1>
        {
            public IDbConnection CreateConnection()
            {
                return new System.Data.SqlClient.SqlConnection(ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection"));
            }
        }
        class Fac2 : IDbConnectionFactory<DB2>
        {
            public IDbConnection CreateConnection()
            {
                return new System.Data.SqlClient.SqlConnection(ConfigurationHelper.Configuration.Value.GetConnectionString("DefaultConnection"));
            }
        }


    }
}
