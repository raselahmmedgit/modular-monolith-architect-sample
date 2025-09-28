using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Dictionary;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel;
using rapid.erp.Repository.Extensions;
using System.Linq.Expressions;


namespace rapid.erp.Repository
{
    public class RepositoryBase
    {
        private readonly AppDbContext _context;
        
        public RepositoryBase()
        {
            //
        }
        public RepositoryBase(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<AppResult> DeleteOrVarifyRecordAsync<T>(int primaryKeyValue, bool doDelete, string columnName = null)
        {
            var columnProperty = _context.FindPrimaryKeyProperty<T>();
            if (columnProperty != null)
            {
                try
                {
                    Type type = typeof(T);
                    ParameterExpression arg = Expression.Parameter(type, "x");
                    var tableName = arg.Type.Name;
                    var primaryKey = columnProperty.Name;
                    var foreignKey = columnName;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        foreignKey = primaryKey;
                    }
                    var tp = columnProperty.ClrType;
                    string spName = DbDictionary.StoredProcedure.ProcedureName.Common.SpDataVarificationAndDelete;
                    var param1 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.TableName, tableName);
                    var param2 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.PrimaryKey, primaryKey);
                    var param3 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.ForeignKey, foreignKey);
                    var param4 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.PrimaryKeyValue, primaryKeyValue);
                    var param5 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.CreatedBy, _context.UserId);
                    var param6 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.DoDelete, doDelete);
                    var data = await _context.AppSqlResult.FromSqlRaw(spName, param1, param2, param3, param4, param5, param6).ToListAsync();

                    if (data != null)
                    {
                        var r = data.FirstOrDefault();
                        if(r.Success)
                            return AppResult.Ok(r.Message);
                        else
                            return AppResult.Fail(r.Message);
                    }
                    return AppResult.Ok();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return AppResult.Fail();
        }

        /// <summary>
        /// Varify Record from tables before delete. You can delete when Success == false and ResultCount =-0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue"></param>
        /// <returns></returns>
        public async Task<AppResult> VarifyRecordBeforDeleteAsync<T>(int primaryKeyValue)
        {
            return await DeleteOrVarifyRecordAsync<T>(primaryKeyValue, false);
        }

        /// <summary>
        /// Delete Or Varify Record from tables. If param doDelete's value is true then it will delete records from table either it will return varification result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue"></param>
        /// <param name="doDelete"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public async Task<AppResult> DeleteOrVarifyRecordAsync<T>(object primaryKeyValue, bool doDelete, string columnName = null)
        {
            var columnProperty = _context.FindPrimaryKeyProperty<T>();
            if (columnProperty != null)
            {
                try
                {
                    Type type = typeof(T);
                    ParameterExpression arg = Expression.Parameter(type, "x");
                    var tableName = arg.Type.Name;

                    var primaryKey = columnProperty.Name;
                    var foreignKey = columnName;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        foreignKey = primaryKey;
                    }
                    //var tp = columnProperty.ClrType;
                    string spName = DbDictionary.StoredProcedure.ProcedureName.Common.SpDataVarificationAndDelete;
                    var param1 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.TableName, tableName);
                    var param2 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.PrimaryKey, primaryKey);
                    var param3 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.ForeignKey, foreignKey);
                    var param4 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.PrimaryKeyValue, primaryKeyValue);
                    var param5 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.CreatedBy, _context.UserId);
                    var param6 = new SqlParameter(DbDictionary.StoredProcedure.ParameterName.DoDelete, doDelete);
                    var data = await _context.AppSqlResult.FromSqlRaw(spName, param1, param2, param3, param4, param5, param6).ToListAsync();
                    if (data != null)
                    {
                        var r = data.FirstOrDefault();
                        return AppResult.Ok(r.Message);
                    }
                    return AppResult.Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return AppResult.Fail();
        }

        /// <summary>
        /// Varify Record from tables before delete. You can delete when Success == false and ResultCount =-0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue"></param>
        /// <returns></returns>
        public async Task<AppResult> VarifyRecordBeforDeleteAsync<T>(object primaryKeyValue)
        {
            return await DeleteOrVarifyRecordAsync<T>(primaryKeyValue, false);
        }

    }
}
