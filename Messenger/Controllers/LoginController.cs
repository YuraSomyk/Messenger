using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messenger.BLL.Services.Jwt.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Messenger.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {

        private IJwtService _jwtService;
        private IConfiguration _config;

        public LoginController(IConfiguration config, IJwtService jwtService) {
            _config = config;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email, string pass) {

            IActionResult response = Unauthorized();

            var user = await _jwtService.AuthenticateUserAsync(email, pass);

            if (user != null) {
                var tokenStr = _jwtService.GenerateJSONWebToken(user, _config);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        [Authorize]
        [HttpPost("Post")]
        public string Post() {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var Name = claim[0].Value;
            return "Welcom " + Name + " =)p";
        }
    }
}