using System.Security.Claims;
using System.Security.Principal;

namespace rapid.erp.Core.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// Check existing claim and add new claim to existing claim.
        /// </summary>
        /// <param name="currentPrincipal"></param>
        /// <param name="key">Claim key/name</param>
        /// <param name="value">Claim value</param>
        public static void AddUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            // authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
        }

        /// <summary>
        /// Check existing claim against claim key/name
        /// </summary>
        /// <param name="currentPrincipal"></param>
        /// <param name="key">Claim key/name</param>
        /// <returns></returns>
        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }
    }
}
