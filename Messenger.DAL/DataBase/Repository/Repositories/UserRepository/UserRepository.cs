using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository.Repositories.UserRepository {

    public class UserRepository : Repository<User, ApplicationContext> {

        public UserRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<object>> GetUsersList() {
            var query = from user in context.Users
                select new { user.Id, user.Name, user.Email, user.Address, user.Phone,
                count = context.Messages.Count(x => x.UserId == user.Id) };

            return await query.ToListAsync();
        }
    }
}