using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.Connections
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
