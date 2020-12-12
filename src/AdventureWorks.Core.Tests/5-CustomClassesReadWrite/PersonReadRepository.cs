using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class PersonReadRepository : ReadDbRepository<Person>
    {
        protected PersonReadRepository() : base(null) { } // Moq requires empty constructor
        public PersonReadRepository(MyDBReadConnection db) : base(db)
        {
        }
        public virtual IEnumerable<Person> GetBestCustomers()
        {
            return this.Query("SELECT TOP 100 * FROM [Person].[Person]");
        }
    }
}
