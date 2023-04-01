using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class MyAppService
    {
        private readonly MyDBConnection _conn;
        public MyAppService(MyDBConnection conn)
        {
            _conn = conn;
        }

        public List<Person> GetUpdatedBestCustomers()
        {
            var repo1 = (PersonRepository) _conn.GetReadWriteRepository<Person>(); // would also work with ReadOnlyRepository

            _conn.CalculateBestCustomers();
            var bestCustomers = repo1.GetBestCustomers();
            return bestCustomers.ToList();
        }
    }
}
