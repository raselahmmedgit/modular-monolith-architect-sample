using Microsoft.AspNetCore.Identity;

namespace rapid.erp.Core.Security
{
    /// <summary>
    /// Application User custom model. Extended from Identity User.
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<string>
    {

    }
}
