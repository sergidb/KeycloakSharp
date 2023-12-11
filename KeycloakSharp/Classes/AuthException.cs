using System;
namespace KeycloakSharp
{
	public class AuthException : Exception
	{
		public AuthException() { }
		public AuthException(string msg) : base(msg) { }
	}
}

