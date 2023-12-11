namespace KeycloakSharp.Classes
{
	public class LoginRequest
	{

		public string? Username { get; set; }
		public string? Password { get; set; }

		public LoginRequest() { }

		public AuthRequest ToAuthRequest()
		{
			return new AuthRequest()
			{
				Username = this.Username,
				Password = Password,
				GrantType = "password"
			};
		}

	}
}

