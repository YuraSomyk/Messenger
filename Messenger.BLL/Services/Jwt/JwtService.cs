using Messenger.BLL.Models;
using Messenger.BLL.Services.Jwt.Interface;
using Messenger.BLL.Services.User.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Jwt
{
    public class JwtService: IJwtService {

        private IUserService _userService { get; set; }

        public JwtService(IUserService userService) {
            _userService = userService;
        }

        public async Task<DTO_User> AuthenticateUserAsync(string Email, string Password) {

            return await _userService.ConfirmPassword(Email, Password);
        }

        public string GenerateJSONWebToken(DTO_User userinfo, IConfiguration _config) {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }
    }
}