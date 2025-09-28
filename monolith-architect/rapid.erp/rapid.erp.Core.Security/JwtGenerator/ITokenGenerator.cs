using System.Security.Claims;
using System.Threading.Tasks;

namespace rapid.erp.Core.Security.JwtGenerator
{
    public interface ITokenGenerator
    {
        Task<TokenModel> CreateTokenAsync(string loginId);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
