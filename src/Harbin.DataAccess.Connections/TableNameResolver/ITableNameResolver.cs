namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Gets table information by the entity type
    /// </summary>
    public interface ITableNameResolver
    {
        /// <summary>
        /// Gets the table name by the entity type
        /// </summary>
        string GetTableName<TEntity>();

        /// <summary>
        /// Sets the table name by the entity type
        /// </summary>
        void SetTableName<TEntity>(string name);
    }
}
