using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;
using Messenger.DAL.DataBase.Repository;
using Messenger.DAL.DataBase.Repository.Interface;
using Messenger.DAL.DataBase.Repository.Repositories;
using Messenger.DAL.Interface;

namespace Messenger.DAL {

    public class UnitOfWork : IUnitOfWork  {

        private AppContext _context;

        public UnitOfWork() {
            _context = new AppContext();
        }

        private Repository<DB_User, AppContext> _users;
        private Repository<DB_Message, AppContext> _messages;

        public IRepository<DB_User> Users => _users ?? (_users = new UserRepository(_context));
        public IRepository<DB_Message> Messages => _messages ?? (_messages = new MessageRepository(_context));

        public void Save() {
            _context.SaveChanges();
        }
    }
}