using Messenger.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.User.Interface {

    public interface IUserService {

        public Task<IEnumerable<DTO_User>> GetUsers();

        public Task<DTO_User> GetUser(int? id);

        public Task<DTO_User> ConfirmPassword(string Email, string Password);

        public Task<string> CreateAsync(DTO_User user);

        public void Dispose();
    }
}