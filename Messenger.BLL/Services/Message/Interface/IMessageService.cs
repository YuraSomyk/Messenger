using Messenger.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Message.Interface {

    public interface IMessageService {

        public Task<IEnumerable<DTO_Message>> GetMessages();

        public Task<DTO_Message> GetMessage(int? id);

        public void Dispose();
    }
}