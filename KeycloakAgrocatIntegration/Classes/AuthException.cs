using System;
namespace KeycloakIntegration
{
	public class AuthException : Exception
	{
		public AuthException() { }
		public AuthException(string msg) : base(msg) { }
	}
}

