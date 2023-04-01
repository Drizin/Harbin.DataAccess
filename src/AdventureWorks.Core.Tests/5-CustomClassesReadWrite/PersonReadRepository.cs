using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Collections.Generic;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class PersonReadRepository : ReadDbRepository<Person>
    {
        protected PersonReadRepository() : base(null) { } // Moq requires empty constructor
        public PersonReadRepository(IReadDbConnection db) : base(db)
        {
        }
        public virtual IEnumerable<Person> GetBestCustomers()
        {
            return this.Query("SELECT TOP 100 * FROM [Person].[Person]");
        }
    }
}
