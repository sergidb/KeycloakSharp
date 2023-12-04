using KeycloakIntegration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar autenticacio amb keycloak
var kcconfig = builder.Configuration.GetSection("Keycloak").Get<KeycloakConfig>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    if (kcconfig.OpenIdConfigURL == null) throw new Exception("No s'ha trobat la configuració OpenID");

    options.MetadataAddress = kcconfig.OpenIdConfigURL;
    options.Authority = kcconfig.RealmURL;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        NameClaimType = "preferred_username"
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); // Use authorization

app.MapControllers();

app.Run();

