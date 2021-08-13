using Auth.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        [HttpGet]
        public IActionResult getMethod()
        {
           return Ok("Api key working");
        }
    }
}
