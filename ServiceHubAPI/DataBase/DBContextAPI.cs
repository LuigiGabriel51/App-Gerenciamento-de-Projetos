using Microsoft.EntityFrameworkCore;
using ServiceHub.Model;
using ServiceHubAPI.Model;
using System.Reflection.Emit;

namespace ServiceHub.DataBase
{
    public class DBContextAPI: DbContext
    {
        public DbSet<UserModel> Users { get; set; } 
        public DbSet<MessageModel> Messages { get; set; } 
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<StageModel> Stages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "server=localhost;user=root;password=1234;database=servicehub";
            var serverVersion = new MySqlServerVersion(new Version(10, 4, 28));
            optionsBuilder.UseMySql(connectionString, serverVersion);
            
        }
    }
}
