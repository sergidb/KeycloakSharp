using System;
namespace KeycloakIntegration { 

    public class KeycloakConfig
    {
        public string? ClientID { get; set; }
        public string? ClientSecret { get; set; }
        public string? Realm { get; set; }
        public string? RealmAdminUsername { get; set; }
        public string? RealmAdminPassword { get; set; }

        public string? BaseURL { get; set; }
        public string? RealmURL { get => this.BaseURL + "/realms/" + Realm; }
        public string? RealmAdminURL { get => BaseURL + "/admin/realms/" + Realm; }
        public string? TokenURL { get => RealmURL + "/protocol/openid-connect/token"; }

        public string? OpenIdConfigURL { get => RealmURL + "/.well-known/openid-configuration"; }

        public KeycloakConfig() { }

        public KeycloakConfig(
            string clientID, string clientSecret, string realm) {

            ClientID = clientID;
            ClientSecret = clientSecret;
            Realm = realm;

        }

        public void SetAdminUser(string username, string password)
        {
            RealmAdminUsername = username;
            RealmAdminPassword = password;
        }

    }
}

