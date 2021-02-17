namespace Messenger.BLL.Models {

    public class DTO_Message {

        public int Id { get; set; }

        public string MessageString { get; set; }

        public int UserId { get; set; }
        public DTO_User User { get; set; }
    }
}