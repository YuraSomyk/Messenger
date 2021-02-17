using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Models;
using Messenger.BLL.Services.Message.Interface;
using Messenger.DAL.DataBase.Models;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Message {

    public class MessageService: IMessageService {

        IUnitOfWork Database { get; set; }

        IMapper Mapper { get; set; }

        public MessageService(IUnitOfWork db) {

            Database = db;

            Mapper = new MapperConfiguration(cfg => { 
                cfg.CreateMap<DAL.DataBase.Models.Message, DTO_Message>();
                cfg.CreateMap<DAL.DataBase.Models.User, DTO_User>(); }
            ).CreateMapper();
        }

        public async Task<IEnumerable<DTO_Message>> GetMessages() {

            return ListMapper(await Database.Messages.GetWithIncludeAsync(x => x.User));
        }

        public async Task<DTO_Message> GetMessage(int? id) {

            if (id == null)
                throw new ValidationException("Not found !", "");

            var message = await Database.Messages.GetEntitesByParams(x=> x.Id == id);

            return ObjectMapper(message);
        }

        private IEnumerable<DTO_Message> ListMapper(IEnumerable<DAL.DataBase.Models.Message> messages) {
            var result = Mapper.Map<IEnumerable<DAL.DataBase.Models.Message>, IEnumerable<DTO_Message>>(messages);

            return result;
        }

        private DTO_Message ObjectMapper(DAL.DataBase.Models.Message message) {
            var result = Mapper.Map<DAL.DataBase.Models.Message, DTO_Message>(message);

            return result;
        }

        public void Dispose() {

            Database.Dispose();
        }   
    }
}