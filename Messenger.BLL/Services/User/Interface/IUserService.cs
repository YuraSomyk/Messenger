using Messenger.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.User.Interface {

    public interface IUserService {

        public Task<DTO_User> ConfirmPassword(string Email, string Password);

        public Task<IEnumerable<object>> GetUsersList();

        public Task<string> CreateAsync(DTO_User user);

        public void Dispose();
    }
}