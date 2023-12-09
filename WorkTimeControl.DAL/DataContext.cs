using Microsoft.EntityFrameworkCore;
using WorkTimeControl.DAL.Entities;

namespace WorkTimeControl.DAL
{
    internal class DataContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserTimeEntity> UserTimes { get; set; }

        public DataContext()
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=WorkTimeControl.db");
        }
    }
}
