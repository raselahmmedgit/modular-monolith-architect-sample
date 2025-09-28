using System;

namespace rapid.erp.Core.Security.JwtGenerator
{
    /// <summary>
    /// Jwt Token Options model.
    /// </summary>
    public class JwtTokenOptions
    {
        public const string Token = "Token";

        public string ValidAudience { get; set; } = String.Empty;
        public string ValidIssuer { get; set; } = String.Empty;
        public string Secret { get; set; } = String.Empty;
        public int TokenValidityInHour { get; set; } = 10000;
        public int RefreshTokenValidityInHour { get; set; } = 10000;
    }
}
