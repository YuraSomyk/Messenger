using Messenger.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Message.Interface {

    public interface IMessageService {

        public Task<IEnumerable<DTO_Message>> GetMessages(int id);

        public Task<string> SendMessage(DTO_Message message);

        public Task<string> DeleteMessage(int id);

        public void Dispose();
    }
}