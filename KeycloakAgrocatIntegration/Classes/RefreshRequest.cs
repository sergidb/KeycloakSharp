using System;
namespace KeycloakIntegration.Classes
{
	public class RefreshRequest : AuthRequest
	{

		public new string? RefreshToken
		{
			get => base.RefreshToken;
			set => base.RefreshToken = value;
		}

		public RefreshRequest()
		{
			GrantType = "refresh_token";
		}

	}
}

