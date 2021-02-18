using AutoMapper;
using Messenger.BLL.Models;
using Messenger.BLL.Services.Password.Interface;
using Messenger.BLL.Services.User.Interface;
using Messenger.DAL.DataBase.Repository.Repositories.UserRepository;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.User {

    public class UserService: IUserService {

        IUnitOfWork Database { get; set; }

        IPasswordService PasswordService { get; set; }

        IMapper Mapper { get; set; }

        public UserService(IUnitOfWork db, IPasswordService passService) {
            Database = db;
            PasswordService = passService;

            Mapper = new MapperConfiguration(cfg => { 
                cfg.CreateMap<DAL.DataBase.Models.User, DTO_User>();
                cfg.CreateMap<DTO_User, DAL.DataBase.Models.User>();
                cfg.CreateMap<object, DTO_User>();
            }).CreateMapper();
        }

        public async Task<IEnumerable<object>> GetUsersList() {
            var users = await ((UserRepository) Database.Users).GetUsersList();

            return users;
        }

        public async Task<string> CreateAsync(DTO_User user) {
            user.Password = PasswordService.Hash(user.Password);
            await Database.Users.Add(Mapper.Map<DTO_User, DAL.DataBase.Models.User>(user));
            Database.Save();
          
            return "Success";
        }

        public async Task<DTO_User> ConfirmPassword(string Email, string Password) {
            var user = await Database.Users.GetEntitesByParams(x => x.Email == Email && x.Password == PasswordService.Hash(Password));

            return Mapper.Map<DAL.DataBase.Models.User, DTO_User>(user);
        }

        public void Dispose() {
            Database.Dispose();
        }
    }
}