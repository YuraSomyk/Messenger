using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Models;
using Messenger.BLL.Services.User.Interface;
using Messenger.DAL.Interface;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Services.User {

    public class UserService: IUserService {

        private string EncryptionKey = "wkepoj82j378;we239fn8cnk12*56&wdoj23nlsfb";

        IUnitOfWork Database { get; set; }

        IMapper Mapper { get; set; }

        public UserService(IUnitOfWork db) {

            Database = db;

            Mapper = new MapperConfiguration(cfg => { 
                cfg.CreateMap<DAL.DataBase.Models.User, DTO_User>();
                cfg.CreateMap<DTO_User, DAL.DataBase.Models.User>();
            }).CreateMapper();
        }

        public async Task<IEnumerable<DTO_User>> GetUsers() {

            return ListMapper(await Database.Users.GetListByParams(null));
        }

        public async Task<DTO_User> GetUser(int? id) {

            if (id == null) throw new ValidationException("Not found !", "");

            var user = await Database.Users.GetEntitesByParams(x => x.Id == id);

            return ObjectMapper(user);
        }

        public async Task<string> CreateAsync(DTO_User user) {
            user.Password = Hash(user.Password);

            await Database.Users.Add(Mapper.Map<DTO_User, DAL.DataBase.Models.User>(user));

            Database.Save();
          
            return "{'result': 'create'}";
        }

        private IEnumerable<DTO_User> ListMapper(IEnumerable<DAL.DataBase.Models.User> users) {
            var result = Mapper.Map<IEnumerable<DAL.DataBase.Models.User>, IEnumerable<DTO_User>>(users);

            return result;
        }

        private DTO_User ObjectMapper(DAL.DataBase.Models.User user) {
            var result = Mapper.Map<DAL.DataBase.Models.User, DTO_User>(user);

            return result;
        }

        public void Dispose() {

            Database.Dispose();
        }

        public async Task<DTO_User> ConfirmPassword(string Email, string Password) {

            var user = await Database.Users.GetEntitesByParams(x => x.Email == Email && x.Password == Hash(Password));

            return ObjectMapper(user);
        }
        
        public string Hash(string clearText) {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create()) {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()) {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = System.Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}