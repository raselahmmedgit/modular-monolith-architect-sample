using rapid.erp.Core.Helpers;
using System.Security.Claims;
using System.Security.Principal;

namespace rapid.erp.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAppUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AppUserId");
            // Test for null to avoid issues during local testing
            return claim.Value.ToString();
        }
        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            // Test for null to avoid issues during local testing
            return claim.Value.ToString();
        }
        public static string GetUserFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("GivenName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetUserFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("GivenName");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetUserLastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("SurName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserNameDisplay(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GivenName);
            // Test for null to avoid issues during local testing
            return (claim != null) ? RapidErpHelper.DecryptString(claim.Value, RapidErpSettings.KeyPassword, RapidErpSettings.SaltPassword) : "No User";
        }
        public static string GetUserIdForLoginId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            // Test for null to avoid issues during local testing
            return claim.Value.ToString();
        }
        public static bool IsSystemAdmin(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("IsSystemAdmin");
            // Test for null to avoid issues during local testing
            return Convert.ToBoolean(claim.Value);
        }
        public static long GetCompanyId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyId");
            // Test for null to avoid issues during local testing
            long tempVal;
            //long? val = long.TryParse(stringVal, out tempVal) ? tempVal : (int?)null;
            return (claim != null) ? long.TryParse(claim.Value, out tempVal) ? tempVal : 0 : 0;
        }
        public static string GetCompanyName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyName");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToString() ?? "Patient Profile Name??";
        }
        public static Guid GetLoginId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("LastLoginId");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToGuid() ?? Guid.Empty;
        }
        public static string GetUserLocalTimeZone(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserLocalTimeZone");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToString() ?? string.Empty;
        }
        public static string GetUserUtcTimeZoneOffset(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserUtcTimeZoneOffset");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToString() ?? string.Empty;
        }
        public static string GetDefaultUtcTimeZoneOffset(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DefaultUtcTimeZoneOffset");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToString() ?? string.Empty;
        }
        public static string GetDefaultAppTimeZoneId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DefaultAppTimeZoneId");
            // Test for null to avoid issues during local testing
            return claim?.Value.ToString() ?? string.Empty;
        }

        public static string GenareteIdentityPassword(this string value)
        {
            List<int> nonSelectedIndexes = new List<int>(Enumerable.Range(0, value.Length));
            Random rand = new Random();

            if (!value.Any(x => char.IsDigit(x)))
            { //does not contain digit
                char[] pass = value.ToCharArray();
                int pos = nonSelectedIndexes[rand.Next(nonSelectedIndexes.Count)];
                nonSelectedIndexes.Remove(pos);
                pass[pos] = Convert.ToChar(rand.Next(10) + '0');
                value = new string(pass);
            }

            if (!value.Any(x => char.IsLower(x)))
            { //does not contain lower
                char[] pass = value.ToCharArray();
                int pos = nonSelectedIndexes[rand.Next(nonSelectedIndexes.Count)];
                nonSelectedIndexes.Remove(pos);
                pass[pos] = Convert.ToChar(rand.Next(26) + 'a');
                value = new string(pass);
            }

            if (!value.Any(x => char.IsUpper(x)))
            { //does not contain upper
                char[] pass = value.ToCharArray();
                int pos = nonSelectedIndexes[rand.Next(nonSelectedIndexes.Count)];
                nonSelectedIndexes.Remove(pos);
                pass[pos] = Convert.ToChar(rand.Next(26) + 'A');
                value = new string(pass);
            }
            return value;
        }
    }
}