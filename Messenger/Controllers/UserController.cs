using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.BLL.Models;
using Messenger.BLL.Services.User.Interface;
using Messenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private IUserService _userService;

        private IMapper Mapper { get; set; }

        public UserController(IUserService userService) {
            _userService = userService;

            Mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, DTO_User>();
                cfg.CreateMap<DTO_User, User>();
            }).CreateMapper();
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<object>> MessageCount() {
            return await _userService.GetUsersList();
        }

        [HttpPost]
        public async Task<string> PostAsync([FromBody] User user) {
            return await _userService.CreateAsync(Mapper.Map<User, DTO_User>(user));
        }
    }
}