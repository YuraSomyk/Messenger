using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;

namespace Messenger.DAL.DataBase.Repository.Repositories {

    public class UserRepository : Repository<DB_User, AppContext> {

        public UserRepository(AppContext context) : base(context) { }
    }
}