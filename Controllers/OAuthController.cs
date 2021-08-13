using Auth.Authorization;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using AllowAnonymousAttribute = Auth.Authorization.AllowAnonymousAttribute;
using AuthorizeAttribute = Auth.Authorization.AuthorizeAttribute;

namespace Auth.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize]
    public class OAuthController : ControllerBase
    {
        private IService _Service;

        public OAuthController(IService Service)
        {
            _Service = Service;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _Service.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate-login")]
        public IActionResult login(AuthenticateRequest model)
        {
            var response = _Service.Login(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _Service.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _Service.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users = _Service.GetAll(null);
            return Ok(users);
        }
        [HttpGet("GetUser")]
        public IActionResult GetbyID( [FromBody] int id)
        {
            var user = _Service.GetById(id);
            return Ok(user);
        }


        // helper methods

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
            {
                var c = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                return c;
            }
        }
    }
}
