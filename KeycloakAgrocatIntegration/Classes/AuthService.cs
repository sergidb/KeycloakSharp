using KeycloakIntegration.Classes;
using RestSharp;

namespace KeycloakIntegration
{
	public class AuthService
	{
        private readonly KeycloakConfig kcconfig;

        public AuthService(KeycloakConfig config) {
            kcconfig = config;
		}

        public AuthResponse Login(LoginRequest request)
        {
            try { return Auth(request.ToAuthRequest()); }
            catch (AuthException ex) { throw ex; }
        }

        public AuthResponse Refresh(RefreshRequest request)
        {
            try { return Auth(request.ToAuthRequest()); }
            catch (AuthException ex) { throw ex; }
        }

		public AuthResponse Auth(AuthRequest request)
		{
			var client = new RestClient();
			var httpReq = new RestRequest(kcconfig.TokenURL)
			{
				Method = Method.Post
			};

            httpReq.AddParameter("client_id", kcconfig.ClientID);
            httpReq.AddParameter("client_secret", kcconfig.ClientSecret);
            httpReq.AddParameter("grant_type", request.GrantType);
            httpReq.AddParameter("scope", request.Scope);

            if (request.Username != null && request.Password != null)
            {
                httpReq.AddParameter("username", request.Username);
                httpReq.AddParameter("password", request.Password);
            }

            if (request.RefreshToken != null)
                httpReq.AddParameter("refresh_token", request.RefreshToken);

            var kcResp = client.Execute<AuthResponse>(httpReq).Data;
            if (kcResp == null)
                throw new AuthException(
                    "Hi ha hagut un problema al contactar amb Keycloak");

            return kcResp;

        }

    }
}

