using Messenger.DAL.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.DAL.DataBase.Context {

    public class ApplicationContext : DbContext {

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=messenger_db;Integrated Security=True");    
        }
    }
}