using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.DataBase.Models.Interface {

    public interface IEntity {
        [Key]
        public int Id { get; set; }
    }
}
