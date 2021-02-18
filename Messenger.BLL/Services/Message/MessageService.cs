using AutoMapper;
using Messenger.BLL.Models;
using Messenger.BLL.Services.Message.Interface;
using Messenger.DAL.DataBase.Repository.Repositories.MessageRepository;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.Message {

    public class MessageService: IMessageService {

        IUnitOfWork Database { get; set; }

        IMapper Mapper { get; set; }

        public MessageService(IUnitOfWork db) {
            Database = db;

            Mapper = new MapperConfiguration(cfg => { 
                cfg.CreateMap<DAL.DataBase.Models.Message, DTO_Message>();
                cfg.CreateMap<DTO_Message, DAL.DataBase.Models.Message>();
                cfg.CreateMap<DAL.DataBase.Models.User, DTO_User>(); 
            }).CreateMapper();
        }

        public async Task<IEnumerable<DTO_Message>> GetMessages(int id) {
            return Mapper.Map<IEnumerable<DAL.DataBase.Models.Message>, IEnumerable<DTO_Message>>
                (await Database.Messages.GetWithIncludeAsync(x => x.User)).Where(x => x.UserId == id);
        }

        public async Task<string> DeleteMessage(int id) {
            await ((MessageRepository) Database.Messages).DeleteMessage(id);
            Database.Save();

            return "Success";
        }

        public async Task<string> SendMessage(DTO_Message message) {
            await Database.Messages.Add(Mapper.Map<DTO_Message, DAL.DataBase.Models.Message> (message));
            Database.Save();

            return "Success";
        }

        public void Dispose() {
            Database.Dispose();
        }   
    }
}