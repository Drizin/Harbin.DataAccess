using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Collections.Generic;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class PersonRepository : ReadWriteDbRepository<Person>
    {
        protected PersonRepository() : base(null) { } // Moq requires empty constructor
        public PersonRepository(IReadWriteDbConnection db) : base(db)
        {
        }
        public virtual IEnumerable<Person> GetBestCustomers()
        {
            return this.Query("SELECT TOP 100 * FROM [Person].[Person]");
        }
    }
}
