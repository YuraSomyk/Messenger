using Messenger.BLL.Services.Message.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Messenger.BLL.Models;
using Messenger.ViewModels;
using AutoMapper;

namespace Messenger.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : ControllerBase {

        private IMessageService _messageService { get; set; }

        private IMapper Mapper { get; set; }

        public MessengerController(IMessageService messageService) {
            _messageService = messageService;

            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTO_Message, Message>();
                cfg.CreateMap<Message, DTO_Message>();
                cfg.CreateMap<DTO_User, User>(); }
             ).CreateMapper();
        }

        [HttpGet][HttpGet("{id}")]
        public async Task<IEnumerable<Message>> Get(int id) {
            return Mapper.Map<IEnumerable<DTO_Message>, List<Message>>(await _messageService.GetMessages(id));
        }

        [HttpPost]
        public async Task<string> SendMessage([FromBody]Message message) {
            return await _messageService.SendMessage(Mapper.Map<Message, DTO_Message>(message));
        }

        [HttpDelete][HttpDelete("{id}")]
        public async Task<string> DeleteMessage(int id) {
            return await _messageService.DeleteMessage(id);
        }
    }
}