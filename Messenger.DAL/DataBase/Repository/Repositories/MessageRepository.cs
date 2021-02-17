using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;

namespace Messenger.DAL.DataBase.Repository.Repositories {

    public class MessageRepository : Repository<Message, ApplicationContext> {

        public MessageRepository(ApplicationContext context) : base(context) { }
    }
}