using System;
namespace KeycloakIntegration.Classes
{
	public class LoginRequest : AuthRequest
	{

		public new string? Username {
			get => base.Username;
			set => base.Username = value;
		}

		public new string? Password
		{
			get => base.Password;
			set => base.Password = value;
		}

		public LoginRequest()
		{
			GrantType = "password";
		}

	}
}

