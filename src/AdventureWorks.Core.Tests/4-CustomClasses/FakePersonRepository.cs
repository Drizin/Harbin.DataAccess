using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using System.Collections.Generic;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class FakePersonRepository : PersonRepository
    {
        public FakePersonRepository(IReadWriteDbConnection db) : base(db)
        {
        }
        public override IEnumerable<Person> GetBestCustomers()
        {
            Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
            return new List<Person>() { p1 };
        }
    }
}
