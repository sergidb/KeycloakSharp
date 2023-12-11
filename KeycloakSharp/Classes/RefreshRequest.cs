namespace KeycloakIntegration.Classes
{
	public class RefreshRequest
	{

		public string? RefreshToken { get; set; }

		public RefreshRequest() { }

		public AuthRequest ToAuthRequest()
		{
			return new AuthRequest()
			{
				GrantType = "refresh_token",
				RefreshToken = this.RefreshToken
			};
		}

	}
}

