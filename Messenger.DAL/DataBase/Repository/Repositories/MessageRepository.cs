using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;

namespace Messenger.DAL.DataBase.Repository.Repositories {

    public class MessageRepository : Repository<DB_Message, AppContext> {

        public MessageRepository(AppContext context) : base(context) { }
    }
}