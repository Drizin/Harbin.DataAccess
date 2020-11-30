using AdventureWorks.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class MyAppService
    {
        private readonly MyDBReadConnection _rconn;
        private readonly MyDBReadWriteConnection _wconn;
        public MyAppService(MyDBReadConnection rconn, MyDBReadWriteConnection wconn)
        {
            _rconn = rconn;
            _wconn = wconn;
        }

        public List<Person> GetUpdatedBestCustomers()
        {
            var repo1 = (PersonReadRepository) _rconn.GetReadRepository<Person>();
            var repo2 = (PersonReadWriteRepository) _wconn.GetReadWriteRepository<Person>();
            
            repo2.CalculateBestCustomers();
            var bestCustomers = repo1.GetBestCustomers();
            return bestCustomers.ToList();
        }
    }
}
