using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeycloakTester.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        // GET: api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("ok");
        }

        [HttpGet("test-no-authorization")]
        public ActionResult<string> GetWithoutAuthorization()
        {
            return Ok("No authorization ok");
        }

        [Authorize]
        [HttpGet("test-authorization")]
        public ActionResult<string> GetWithAuthorization()
        {
            return Ok("Authorized ok");
        }
    }
}

