using System;
namespace KeycloakSharp
{
	public class AuthResponse
	{
        public AuthResponse()
        {
            IssueDate = DateTime.Now;
        }

        public string? Access_token { get; set; }
        public int Expires_in { get; set; }
        public int Refresh_expires_in { get; set; }
        public string? Refresh_token { get; set; }
        public string? Token_type { get; set; }
        public string? Id_token { get; set; }
        public string? Session_state { get; set; }
        public string? Scope { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime AccessExpirationDate { get => IssueDate.AddSeconds(Expires_in); }
        public DateTime RefreshExpirationDate { get => IssueDate.AddSeconds(Refresh_expires_in); }

        public string? TokenType { get; set; }

    }
}

