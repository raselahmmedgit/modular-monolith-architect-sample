using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using System.Collections.Generic;
using System.Linq;

namespace rapid.erp.Repository.Extensions
{
    public static class EntityExtensions
    {
        public static string FindEntityByNames<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault().Name;
        }
        public static IEnumerable<string> FindPrimaryKeyNames<T>(this DbContext dbContext, T entity)
        {
            return from p in dbContext.FindPrimaryKeyProperties(entity)
                   select p.Name;
        }

        public static IEnumerable<object> FindPrimaryKeyValues<T>(this DbContext dbContext, T entity)
        {
            return from p in dbContext.FindPrimaryKeyProperties(entity)
                   select entity.GetPropertyValue(p.Name);
        }

        static IReadOnlyList<IProperty> FindPrimaryKeyProperties<T>(this DbContext dbContext, T entity)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        }
        public static IProperty FindPrimaryKeyProperty<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
        }
        static object GetPropertyValue<T>(this T entity, string name)
        {
            return entity.GetType().GetProperty(name).GetValue(entity, null);
        }

    }
}
