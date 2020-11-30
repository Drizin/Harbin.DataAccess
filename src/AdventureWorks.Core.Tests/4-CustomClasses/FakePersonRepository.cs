using AdventureWorks.Core.Domain.Entities;
using Harbin.Infrastructure.Database.Connection;
using Harbin.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class FakePersonRepository : PersonRepository
    {
        public FakePersonRepository(MyDBConnection db) : base(db)
        {
        }
        public override IEnumerable<Person> GetBestCustomers()
        {
            Person p1 = new Person() { FirstName = "Rick", LastName = "Drizin" };
            return new List<Person>() { p1 };
        }
    }
}
