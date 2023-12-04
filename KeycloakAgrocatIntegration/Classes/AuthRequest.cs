using System;
namespace KeycloakIntegration
{
	public class AuthRequest
	{
        public string? GrantType { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public string? Scope { get; set; } = "openid";
    }
}

