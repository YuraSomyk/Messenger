using Messenger.BLL.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Jwt.Interface {

    public interface IJwtService {

        public Task<DTO_User> AuthenticateUserAsync(string Email, string Password);

        public string GenerateJSONWebToken(DTO_User userinfo, IConfiguration _config);
    }
}
