using System;
using Messenger.DAL.DataBase.Context;
using Messenger.DAL.DataBase.Models;
using Messenger.DAL.DataBase.Repository;
using Messenger.DAL.DataBase.Repository.Interface;
using Messenger.DAL.DataBase.Repository.Repositories;
using Messenger.DAL.Interface;

namespace Messenger.DAL {

    public class UnitOfWork : IUnitOfWork  {

        private ApplicationContext _context;

        public UnitOfWork() {
            _context = new ApplicationContext();
        }

        private Repository<User, ApplicationContext> _users;
        private Repository<Message, ApplicationContext> _messages;

        public IRepository<User> Users => _users ?? (_users = new UserRepository(_context));
        public IRepository<Message> Messages => _messages ?? (_messages = new MessageRepository(_context));

        public void Save() {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}