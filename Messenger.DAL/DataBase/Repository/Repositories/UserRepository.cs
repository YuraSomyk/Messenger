using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;

namespace Messenger.DAL.DataBase.Repository.Repositories {

    public class UserRepository : Repository<User, ApplicationContext> {

        public UserRepository(ApplicationContext context) : base(context) { }
    }
}