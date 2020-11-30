using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Repositories encapsulate storage/retrieval/search of objects.
    /// 
    /// While <see cref="IReadOnlyDbRepository{TEntity}"/> only offers read methods, this one also offers write methods (Insert/Update/Delete).
    /// 
    /// Insert/Update/Delete are implemened by FastCRUD, but you can override those methods write your own.
    /// 
    /// ReadDbRepository{TEntity}/ReadWriteDbRepository{TEntity} can be used in multiple ways:
    /// - You can use those classes directly and use their methods ("Query", "Insert", "Update", etc).
    /// - You can extend it using extension methods to create reusable Queries methods, QueryObjects, DbCommands.
    /// - You can extend it using inheritance to create reusable Queries, QueryObjects, DbCommands, which can be overriden in derived Mock classes.
    /// 
    /// ReadWriteDbRepository{TEntity} implements the "Generic Repository Pattern" because it uses FastCRUD to automatically
    /// generate the INSERT/UPDATE/DELETE statements.
    /// </summary>
    public interface IReadWriteDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
        #region Insert/Update/Delete (which are automatically implemented by FastCRUD - but you can override and write on your own!)

        /// <summary>
        /// Inserts a new record. Default implementation (unless overriden) uses Dapper FastCRUD
        /// </summary>
        TEntity Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// Updates a record by the primary key. Default implementation (unless overriden) uses Dapper FastCRUD
        /// </summary>
        bool Update(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// Deletes record by the primary key. Default implementation (unless overriden) uses Dapper FastCRUD
        /// </summary>
        bool Delete(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);
        #endregion

        #region Dapper.FastCRUD specific methods
        /// <summary>
        /// Bulk update (multiple records) by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        int BulkUpdate(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Bulk update (multiple records) by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        Task<int> BulkUpdateAsync(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Bulk delete (multiple records) by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        int BulkDelete(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Bulk delete (multiple records) by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        Task<int> BulkDeleteAsync(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null);
        #endregion

        #region Execute Methods
        /// <summary>
        /// Executes the command (using Dapper), returning the number of rows affected.
        /// Warning: this uses the underlying read-write connection and can use ANY table, so it's up to the developer to write the commands in the correct repository.
        /// </summary>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the command (using Dapper), returning the number of rows affected.
        /// Warning: this uses the underlying read-write connection and can use ANY table, so it's up to the developer to write the commands in the correct repository.
        /// </summary>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        #endregion

    }
}
