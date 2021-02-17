using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Models;
using Messenger.BLL.Services.User.Interface;
using Messenger.DAL.DataBase.Models;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.User {

    public class UserService: IUserService {

        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork db) {
            Database = db;
        }

        public async Task<IEnumerable<DTO_User>> GetUsers() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.DataBase.Models.User, DTO_User>()).CreateMapper();
            return mapper.Map<IEnumerable<DAL.DataBase.Models.User>, List<DTO_User>>(await Database.Users.GetAll());
        }
 
        public async Task<DTO_User> GetUser(int? id) {
            if (id == null)
                throw new ValidationException("Not found !", "");

            var user = await Database.Users.Get(id.Value);

            if (user == null)
                throw new ValidationException("Not found !", "");
             
            return new DTO_User { Id = user.Id, Name = user.Name, Address = user.Address, Email = user.Email, Phone = user.Phone };
        }
 
        public void Dispose() {
            Database.Dispose();
        }
    }
}