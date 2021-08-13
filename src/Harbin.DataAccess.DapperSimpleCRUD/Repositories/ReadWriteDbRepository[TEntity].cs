using Harbin.DataAccess.DapperSimpleCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperSimpleCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadWriteDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadWriteDbRepository<TEntity>
    {
        new protected readonly IReadWriteDbConnection _db;
        static protected List<PropertyInfo> _entityKeys;
        static protected PropertyInfo _identityProperty = null;
        static protected MethodInfo _insertMethod;
        static protected MethodInfo _insertAsyncMethod;

        public ReadWriteDbRepository(IReadWriteDbConnection db) : base(db)
        {
            _db = db;
        }

        static ReadWriteDbRepository()
        {
            _entityKeys = ((IEnumerable<PropertyInfo>) 
                typeof(Dapper.SimpleCRUD)
                .GetMethod("GetIdProperties", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(Type) }, null)
                .Invoke(null, new object[] { typeof(TEntity) })).ToList();

            Type tKey = typeof(int);
            if (_entityKeys.Count == 1)
            {
                _identityProperty = _entityKeys.Single();
                tKey = _identityProperty.PropertyType;
            }

            _insertMethod = typeof(Dapper.SimpleCRUD).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Single(m => m.Name == "Insert" && m.GetGenericArguments().Count() == 2)
                .MakeGenericMethod(tKey, typeof(TEntity));

            _insertAsyncMethod = typeof(Dapper.SimpleCRUD).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Single(m => m.Name == "InsertAsync" && m.GetGenericArguments().Count() == 2)
                .MakeGenericMethod(tKey, typeof(TEntity));
        }

        #region Insert/Update/Delete using FastCRUD (you can override and write on your own!)

        /// <inheritdoc/>
        public virtual TEntity Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            //var result = Dapper.SimpleCRUD.Insert(_db, entity, transaction, commandTimeout);
            var result = _insertMethod.Invoke(null, new object[] { _db, entity, transaction, commandTimeout });
            _identityProperty?.SetValue(entity, result, null);
            return entity;
        }


        /// <inheritdoc/>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            //var result =  await Dapper.SimpleCRUD.InsertAsync(_db, entity, transaction, commandTimeout);
            dynamic awaitable = _insertAsyncMethod.Invoke(null, new object[] { _db, entity, transaction, commandTimeout });
            var result = await awaitable;
            _identityProperty?.SetValue(entity, result, null);
            return entity; 
        }

        /// <inheritdoc/>
        public virtual bool Update(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.Update(_db, entity, transaction, commandTimeout) == 1;
        }

        /// <inheritdoc/>
        public async virtual Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken? token = null)
        {
            return await Dapper.SimpleCRUD.UpdateAsync(_db, entity, transaction, commandTimeout, token) == 1;
        }

        /// <inheritdoc/>
        public virtual bool Delete(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.Delete(_db, entity, transaction, commandTimeout) == 1;
        }

        /// <inheritdoc/>
        public async virtual Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await Dapper.SimpleCRUD.DeleteAsync(_db, entity, transaction, commandTimeout) == 1;
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

        #region Dapper.SimpleCRUD specific methods

        /// <inheritdoc/>
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.DeleteList<TEntity>(_db, conditions, parameters, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.DeleteList<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.DeleteListAsync<TEntity>(_db, conditions, parameters, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.DeleteListAsync<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        #endregion



    }
}
