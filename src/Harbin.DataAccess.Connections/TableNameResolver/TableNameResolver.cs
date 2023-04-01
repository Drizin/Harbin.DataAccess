using System.Collections.Generic;
using System;

namespace Harbin.DataAccess.Connections
{
    /// <inheritdoc/>
    public class TableNameResolver : ITableNameResolver
    {
        private readonly Dictionary<Type, string> _registeredTypes = new Dictionary<Type, string>();

        /// <inheritdoc/>
        public string GetTableName<TEntity>()
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return _registeredTypes[typeof(TEntity)];
            return typeof(TEntity).Name;
        }

        /// <inheritdoc/>
        public void SetTableName<TEntity>(string name)
        {
            _registeredTypes.Add(typeof(TEntity), name);
        }
    }
}
