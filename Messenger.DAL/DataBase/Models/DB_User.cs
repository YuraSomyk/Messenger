﻿using Messenger.DAL.DataBase.Models.Interface;

namespace Messenger.DAL.DataBase.Models {

    public class DB_User: IEntity {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
