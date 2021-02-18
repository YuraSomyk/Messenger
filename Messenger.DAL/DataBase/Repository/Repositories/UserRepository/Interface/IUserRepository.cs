using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository.Repositories.UserRepository.Interface {

    public interface IUserRepository {

        Task<IEnumerable<object>> GetUsersList();
    }
}