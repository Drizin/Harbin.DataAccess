using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Harbin.DataAccess.DapperFastCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperFastCRUD.Repositories
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
        
        /// <inheritdoc/>
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


        /// <inheritdoc/>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            await Dapper.FastCrud.DapperExtensions.InsertAsync(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
            return entity;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public virtual Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.FastCrud.DapperExtensions.UpdateAsync(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public virtual Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.FastCrud.DapperExtensions.DeleteAsync(_db, entity, o =>
            {
                if (transaction != null)
                    o.AttachToTransaction(transaction);
                if (commandTimeout != null)
                    o.WithTimeout(TimeSpan.FromSeconds(commandTimeout.Value));
            });
        }
        #endregion

        #region Dapper.FastCRUD specific methods

        /// <inheritdoc/>
        public virtual int BulkUpdate(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkUpdate(_db, updateData, statementOptions);
        }

        /// <inheritdoc/>
        public virtual Task<int> BulkUpdateAsync(TEntity updateData, Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkUpdateAsync(_db, updateData, statementOptions);
        }

        /// <inheritdoc/>
        public virtual int BulkDelete(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkDelete(_db, statementOptions);
        }

        /// <inheritdoc/>
        public virtual Task<int> BulkDeleteAsync(Action<IConditionalBulkSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.BulkDeleteAsync(_db, statementOptions);
        }
        #endregion

        #region Execute Methods
        /// <inheritdoc/>
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.Execute(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.ExecuteAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion


    }
}
