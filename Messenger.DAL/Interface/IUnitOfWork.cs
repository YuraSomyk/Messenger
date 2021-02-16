using Messenger.DAL.DataBase.Models;
using Messenger.DAL.DataBase.Repository.Interface;

namespace Messenger.DAL.Interface {

    public interface IUnitOfWork {

        IRepository<DB_User> Users { get; }

        IRepository<DB_Message> Messages { get; }

        void Save();
    }
}