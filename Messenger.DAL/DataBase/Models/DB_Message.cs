using Messenger.DAL.DataBase.Models.Interface;

namespace Messenger.DAL.DataBase.Models {

    public class DB_Message: IEntity {

        public int Id { get; set; }

        public string Message { get; set; }

        public int userId { get; set; }
        public DB_User User { get; set; }
    }
}
