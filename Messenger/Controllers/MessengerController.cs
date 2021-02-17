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

        public MessangerController(IMessageService messageService) {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<List<Message>> Get() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DTO_Message, Message>()).CreateMapper();
            return mapper.Map<IEnumerable<DTO_Message>, List<Message>>(await _messageService.GetMessages());
        }
    }
}