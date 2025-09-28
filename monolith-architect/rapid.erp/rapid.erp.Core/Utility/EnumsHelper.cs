using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace rapid.erp.Core.Utility
{
    public static class EnumsHelper
    {
        /// <summary>
        /// Get Display Name from data Annotations attribute used in the view models.  
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns>Return Display Name attribute value if defined or empty</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }
    }
}
