using Messenger.BLL.Services.Message.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Messenger.BLL.Models;
using Messenger.ViewModels;
using AutoMapper;

namespace Messenger.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class MessangerController : ControllerBase {

        private IMessageService _messageService;
        IMapper Mapper { get; set; }

        public MessangerController(IMessageService messageService) {
            _messageService = messageService;

            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTO_Message, Message>();
                cfg.CreateMap<DTO_User, User>(); }
             ).CreateMapper();
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get() {
            return ListMapper(await _messageService.GetMessages());
        }

        private IEnumerable<Message> ListMapper(IEnumerable<DTO_Message> messages) {
            var result = Mapper.Map<IEnumerable<DTO_Message>, List<Message>>(messages);

            return result;
        }

        private Message ObjectMapper(DTO_Message message) {
            var result = Mapper.Map<DTO_Message, Message>(message);

            return result;
        }
    }
}