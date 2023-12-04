
# Keycloak integration into .NET 6 WebApi:

## 1. Key concepts
- **Controller:**  
A Controller is a component in your application that handles incoming requests, processes them, and returns an appropriate response. It acts as an intermediary between the user interface and the application's business logic.  

- **Endpoint:**  
An Endpoint is a specific URL or URI (Uniform Resource Identifier) in your application that is used to interact with specific functionalities or resources.

- **Autentication:**  
Authentication is the process of verifying the identity of users or systems attempting to access a secure environment. It ensures that users are who they claim to be before granting access. Common methods include username/password, tokens, or certificates.

- **Autorization:**  
Authorization is the process of determining whether an authenticated user or system has the necessary permissions to perform a requested action or access a specific resource. It defines what users are allowed to do within an application or system based on their roles or attributes.


## 2. Project Configuration

1. Add the KeycloakIntegration DLL to your WebApi project.
1. Install the [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/) NuGet package.

1. Add Keycloak configuration to appsettings.json
     - Replace `<client_id>`, `<client_secret>`, `<keycloak_server_url>` and `<realm_name>` with your values.
    ```json
    "Keycloak": {
        "clientID": "<client_id>",
        "clientSecret": "<client_secret>",
        "baseURL": "<keycloak_server_url>",
        "realm": "<realm_name>",
        "realmAdminUsername": "",
        "realmAdminPassword": ""
    }
    ```

1. Create the AuthController with the following content:
    - Replace `<namespace>` for the namespace of your project.
    ```csharp
    using KeycloakIntegration;
    using KeycloakIntegration.Classes;
    using Microsoft.AspNetCore.Mvc;

    namespace <namespace> /* Replace the namespace !!! */
    {
        [Route("api/[controller]")]
        public class AuthController : Controller
        {
            private readonly AuthService auth;

            public AuthController(IConfiguration configuration)
            {
                var kcconfig = configuration.GetSection("Keycloak").Get<KeycloakConfig>();
                auth = new(kcconfig);
            }

            public ActionResult<AuthResponse> Post([FromForm] string grant_type,
                                                [FromForm] string? username,
                                                [FromForm] string? password,
                                                [FromForm] string? refresh_token)
            {
                var authReq = new AuthRequest()
                {
                    GrantType = grant_type,
                    Username = username,
                    Password = password,
                    RefreshToken = refresh_token
                };

                try
                {
                    var resp = auth.Auth(authReq);
                    return Ok(resp);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpPost("")]
            public IActionResult Login(LoginRequest request)
            {
                try { return Ok(auth.Login(request)); }
                catch(Exception) { throw; }
            }

            [HttpPost("")]
            public IActionResult Refresh(RefreshRequest request)
            {
                try { return Ok(auth.Refresh(request)); }
                catch (Exception) { throw; }
            }
        }
    }
    ```

1. Add the following code to the `Program.cs` file before the `builder.Services.AddControllers()` line:
    ```csharp
    // Configure authentication with Keycloak
    var kcconfig = builder.Configuration.GetSection("Keycloak").Get<KeycloakConfig>();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        if (kcconfig.OpenIdConfigURL == null) throw new Exception("Keycloak configuration not found in appsettings.json");

        options.MetadataAddress = kcconfig.OpenIdConfigURL;
        options.Authority = kcconfig.RealmURL;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            NameClaimType = "preferred_username"
        };
    });
    ```

1. Add `app.UseAuthentication()` before `app.UseAuthorization()` in `Program.cs`;

## 3. Securing Controllers or Endpoints:

You can enforce authentication either at controller level or at endpoint level.

### At controller level
Place the `[Authorize]` attribute just before the class declaration:
```csharp
[Route("api/[controller]")]
[Authorize]
public class StatusController : Controller
{
    // [...]
```

### At endpoint level
Place the `[Authorize]` attribute just before the method declaration:
```csharp
[Authorize]
[HttpGet("test-authorization")]
public ActionResult<string> GetWithAuthorization()
{
    // [...]
```
_(This is equivalent to placing `[Authorize]` on all methods of the controller.)_
