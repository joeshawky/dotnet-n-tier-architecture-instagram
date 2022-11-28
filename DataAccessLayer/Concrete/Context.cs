using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = LocalSettings.GetConnectionString("ContextSettings.json");
        optionsBuilder.UseSqlServer(connectionString);
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<>
    //}
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<RiskyComment> RiskyComments { get; set; }
    public DbSet<ProfileImage> ProfileImages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<PostSave> PostSaves { get; set; }
    public DbSet<ChatInstance> chatInstances { get; set; }

}