using Messenger.DAL.DataBase.Models.Interface;

namespace Messenger.DAL.DataBase.Models {

    public class Message: IEntity {

        public int Id { get; set; }

        public string MessageString { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}