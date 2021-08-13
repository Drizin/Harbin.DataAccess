using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperFastCRUD.Connections
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
