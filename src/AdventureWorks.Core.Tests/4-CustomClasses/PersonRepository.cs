using AdventureWorks.Core.Domain.Entities;
using Harbin.Infrastructure.Database.Connection;
using Harbin.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class PersonRepository : ReadWriteDbRepository<Person>
    {
        protected PersonRepository() : base(null) { } // Moq requires empty constructor
        public PersonRepository(MyDBConnection db) : base(db)
        {
        }
        public virtual IEnumerable<Person> GetBestCustomers()
        {
            return this.Query("SELECT TOP 100 * FROM [Person].[Person]");
        }
    }
}
