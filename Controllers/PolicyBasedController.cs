using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Greaterthan27")]
    public class PolicyBasedController : ControllerBase
    {
        [HttpGet]
        public IActionResult getMethod()
        {
            return Ok(" Ok ki report hai. ");
        }
    }
}
