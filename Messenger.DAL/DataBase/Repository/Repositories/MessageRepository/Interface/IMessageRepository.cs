using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository.Repositories.MessageRepository.Interface {

    public interface IMessageRepository {

        Task<bool> DeleteMessage(int id);
    }
}