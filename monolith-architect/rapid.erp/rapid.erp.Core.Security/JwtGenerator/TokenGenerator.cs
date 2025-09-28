using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace rapid.erp.Core.Security.JwtGenerator
{
    /// <summary>
    /// Token Generation middleware. Please do not change anything here.
    /// </summary>
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private IUserClaimsPrincipalFactory<ApplicationUser> _appClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenGenerator(IUserClaimsPrincipalFactory<ApplicationUser> appClaimsPrincipalFactory, IOptions<JwtTokenOptions> jwtTokenOptions, UserManager<ApplicationUser> userManager)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
            _appClaimsPrincipalFactory = appClaimsPrincipalFactory;
            _userManager = userManager;
        }

        public async Task<TokenModel> CreateTokenAsync(string loginId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginId);
                if (user != null)
                {
                    var auth = await _appClaimsPrincipalFactory.CreateAsync(user);

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret));
                    var token = new JwtSecurityToken(
                        issuer: _jwtTokenOptions.ValidIssuer,
                        audience: _jwtTokenOptions.ValidAudience,
                        expires: DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                        claims: auth.Claims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                    return new TokenModel
                    {
                        AccessToken = token,
                        ExpiryDate = DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                        RefreshTokenModel = GenerateRefreshToken(),
                        AddedDate = DateTime.Now
                    };
                }
                return new TokenModel();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public RefreshTokenModel GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var val = Convert.ToBase64String(randomNumber);
            return new RefreshTokenModel
            {
                RefreshToken = val,
                ExpiryDate = DateTime.Now.AddHours(_jwtTokenOptions.RefreshTokenValidityInHour),
                AddedDate = DateTime.Now
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
