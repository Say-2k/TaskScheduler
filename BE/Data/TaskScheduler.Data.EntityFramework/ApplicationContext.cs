using Microsoft.EntityFrameworkCore;

namespace TaskScheduler.Data.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Common.Models.DBModels.Task> Task { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql(ConnectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Common.Models.DBModels.Task>(entity =>
            {
                entity.HasKey(t => t.Id);
            });
        }
    }
}