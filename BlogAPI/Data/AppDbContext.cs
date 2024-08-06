using BlogAPI.Model.Domain;
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
            modelBuilder.Entity<user>().HasOne(c => c.credential).WithOne(c => c.user).HasForeignKey<credential>(c => c.user_id);
            modelBuilder.Entity<post>().HasOne(u => u.user).WithMany(p => p.post).HasForeignKey(pi => pi.post_id);
            modelBuilder.Entity<post>().HasMany(v => v.volume).WithOne(p => p.post).HasForeignKey(pi => pi.post_id);
            modelBuilder.Entity<volume>().HasMany(v => v.chapter).WithOne(p => p.volume).HasForeignKey(pi => pi.volume_id);
            modelBuilder.Entity<post_category_temp>().HasOne(c => c.category).WithMany(c => c.post_category_temp).HasForeignKey(pi => pi.category_id);
            modelBuilder.Entity<post_category_temp>().HasOne(c => c.post).WithMany(c => c.post_category_temp).HasForeignKey(pi => pi.post_id);
            modelBuilder.Entity<credential>().HasOne(r => r.role).WithMany(c => c.credential).HasForeignKey(ri => ri.cred_roleid);
        }
        public DbSet<post> post { get; set; }
        public DbSet<chapter> chapter { get; set; }
        public DbSet<volume> volume { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<category> category { get; set; }
        public DbSet<post_category_temp> post_category_temp { get; set; }
        public DbSet<credential> credential { get; set; }
        public DbSet<credential> role { get; set; }
    }
}