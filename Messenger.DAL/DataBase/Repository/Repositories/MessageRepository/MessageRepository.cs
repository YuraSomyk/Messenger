using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;
using Messenger.DAL.DataBase.Repository.Repositories.MessageRepository.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository.Repositories.MessageRepository {

    public class MessageRepository: Repository<Message, ApplicationContext>, IMessageRepository {

        public MessageRepository(ApplicationContext context) : base(context) { }

        public async Task<bool> DeleteMessage(int id) {
            var messages = (from message in context.Messages.ToList()
                     where message.UserId == id
                     select message).ToList();

            context.Messages.RemoveRange(messages);
            await context.SaveChangesAsync();

            return true;
        }
    }
}