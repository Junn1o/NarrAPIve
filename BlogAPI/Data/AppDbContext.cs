using BlogAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<user>().HasOne(c => c.credential).WithOne(c => c.user).HasForeignKey<user>(c => c.user_id);
            modelBuilder.Entity<post>().HasOne(u => u.user).WithMany(p=>p.post).HasForeignKey(pi=>pi.post_id);
            modelBuilder.Entity<post>().HasMany(v => v.volume).WithOne(p => p.post).HasForeignKey(pi => pi.post_id);
            modelBuilder.Entity<volume>().HasMany(v => v.chapter).WithOne(p => p.volume).HasForeignKey(pi => pi.volume_id);
            modelBuilder.Entity<post_category_temp>().HasOne(c => c.category).WithMany(c => c.post_category_temp).HasForeignKey(pi=>pi.category_id);
            modelBuilder.Entity<post_category_temp>().HasOne(c => c.post).WithMany(c => c.post_category_temp).HasForeignKey(pi => pi.post_id);
        }
        public DbSet<post> Posts { get; set; }
        public DbSet<chapter> Chapters { get; set; }
        public DbSet<volume> Volumes { get; set; }
        public DbSet<user> Users { get; set; }
        public DbSet<category> Categories { get; set; }
        public DbSet<post_category_temp> PostCategories { get; set; }
        public DbSet<credential> Credentials { get; set; }
    }
}
