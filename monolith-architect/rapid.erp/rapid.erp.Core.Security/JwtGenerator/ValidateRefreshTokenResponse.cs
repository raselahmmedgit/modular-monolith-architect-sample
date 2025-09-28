using rapid.erp.Core.Security.ViewModel;

namespace rapid.erp.Core.Security.JwtGenerator
{
    /// <summary>
    /// Jwt token validation response
    /// </summary>
    public class ValidateRefreshTokenResponse : BaseResponse
    {
        public string UserId { get; set; }
    }
}
