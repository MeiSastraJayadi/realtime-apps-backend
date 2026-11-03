using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeApps.Models;

namespace RealTimeApps.Infrastructures.Persistence;

public class AppDbContext : IdentityDbContext<PersonalUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Server>(entity =>
        {

            entity.Property("CreatedAt")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            entity.HasOne(s => s.Owner)
                .WithMany(u => u.Servers)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PersonalUser>(entity =>
        {
           entity.Property("CreatedAt")
            .HasDefaultValueSql("NOW()") 
            .ValueGeneratedOnAdd();

            entity.Property("UpdatedAt")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();
        });

        builder.Entity<Channel>(entity =>
        {
            entity.Property("CreatedAt")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            entity.HasOne(c => c.Server)
                .WithMany(s => s.Channels)
                .HasForeignKey(s => s.ServerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Message>(entity =>
        {
            
            entity.Property("CreatedAt")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
            
            entity.HasOne(m => m.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChannelId);

            entity.HasOne(m => m.PersonalUser)
                .WithMany()
                .HasForeignKey(m => m.PersonalUserId);
        });

        builder.Entity<ServeMembers>(entity =>
        {
            entity.HasKey(sm => new
            {
                sm.PersonalUserId, 
                sm.ServerId
            });

            entity.Property("JoinAt")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
            
            entity.HasOne<PersonalUser>()
                .WithMany(u => u.ServeMembers)
                .HasForeignKey(sm => sm.PersonalUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne<Server>()
                .WithMany(s => s.ServeMembers)
                .HasForeignKey(sm => sm.ServerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }

    public DbSet<PersonalUser> PersonalUser { get; set; }
    public DbSet<Server> Server { get; set; }
    public DbSet<Channel> Channel { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<ServeMembers> ServeMembers { get; set; }
}
