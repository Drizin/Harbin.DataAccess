using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Harbin.Infrastructure.Database.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class ReadWriteDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadWriteDbRepository<TEntity>
    {
        new protected readonly IReadWriteDbConnection _db;

        public ReadWriteDbRepository(IReadWriteDbConnection db) : base(db)
        {
            _db = db;
        }

        #region Insert/Update/Delete using FastCRUD (you can override and write on your own!)
        public virtual TEntity Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            Dapper.FastCrud.DapperExtensions.Insert(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
            return entity;
        }
        public virtual bool Update(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.FastCrud.DapperExtensions.Update(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
        }

        public virtual bool Delete(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.FastCrud.DapperExtensions.Delete(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
        }
        #endregion

        #region Dapper.FastCRUD specific methods
        public virtual int BulkUpdate(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkUpdate(_db, updateData, statementOptions);
        }
        public virtual Task<int> BulkUpdateAsync(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkUpdateAsync(_db, updateData, statementOptions);
        }
        public virtual int BulkDelete(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkDelete(_db, statementOptions);
        }
        public virtual Task<int> BulkDeleteAsync(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkDeleteAsync(_db, statementOptions);
        }
        #endregion

        #region Execute Methods
        // By exposing Execute Methods directly the developer could change ANY table, which probably should be used directly with IReadWriteDbConnection
        // (and not IReadWriteDbRepository<TEntity>)
        /*
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.Execute(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.ExecuteAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        */
        #endregion


    }
}
