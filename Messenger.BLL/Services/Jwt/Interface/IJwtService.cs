using Messenger.BLL.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Jwt.Interface {

    public interface IJwtService {

        Task<DTO_User> AuthenticateUserAsync(string Email, string Password);

        string GenerateJSONWebToken(DTO_User userinfo, IConfiguration _config);
    }
}
