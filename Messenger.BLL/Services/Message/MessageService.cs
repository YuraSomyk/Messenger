using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Models;
using Messenger.BLL.Services.Message.Interface;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Message {

    public class MessageService: IMessageService {

        IUnitOfWork Database { get; set; }

        public MessageService(IUnitOfWork db) {
            Database = db;
        }

        public async Task<IEnumerable<DTO_Message>> GetMessages() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.DataBase.Models.Message, DTO_Message>()).CreateMapper();
            return mapper.Map<IEnumerable<DAL.DataBase.Models.Message>, List<DTO_Message>>(await Database.Messages.GetAll());
        }
 
        public async Task<DTO_Message> GetMessage(int? id) {
            if (id == null)
                throw new ValidationException("Not found !", "");

            var message = await Database.Messages.Get(id.Value);

            if (message == null)
                throw new ValidationException("Not found !", "");
             
            return new DTO_Message { Id = message.Id, Message = message.MessageString, UserId = message.UserId };
        }
 
        public void Dispose() {
            Database.Dispose();
        }
    }
}