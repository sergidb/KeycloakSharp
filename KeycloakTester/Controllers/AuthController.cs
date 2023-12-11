using KeycloakSharp;
using KeycloakSharp.Classes;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakTester.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly AuthService auth;

        public AuthController(IConfiguration configuration)
        {
            // Obtenim la configuració del fitxer appsettings.json
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

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            try { return Ok(auth.Login(request)); }
            catch(Exception) { throw; }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshRequest request)
        {
            try { return Ok(auth.Refresh(request)); }
            catch (Exception) { throw; }
        }

    }
}

