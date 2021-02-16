using Messenger.DAL.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.DAL.DataBase.Context {

    public class AppContext : DbContext {

        public DbSet<DB_User> Users { get; set; }

        public DbSet<DB_Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=(localdb)\\ mssqllocaldb; Database=messanger_db; Trusted_Connection=True");
        }
    }
}