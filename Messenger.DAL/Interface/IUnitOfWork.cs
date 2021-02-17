using Messenger.DAL.DataBase.Models;
using Messenger.DAL.DataBase.Repository.Interface;
using System;

namespace Messenger.DAL.Interface {

    public interface IUnitOfWork : IDisposable {

        IRepository<User> Users { get; }

        IRepository<Message> Messages { get; }

        void Save();
    }
}