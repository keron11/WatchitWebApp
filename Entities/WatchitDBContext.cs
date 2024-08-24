using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using WebSite.Common;

namespace WatchitWebApp.Entities
{
    public class WatchitDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public WatchitDBContext(DbContextOptions<WatchitDBContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Watchit;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            User[] users = new User[] {
                new User(){ Id=1, Name="Max", Login="zera", Username="zeratul", Password=SecurePasswordHasher.Hash("qwerty"), Email="m@gmail.com", DateCreated=DateTime.Now,  IsVerified=true},
                new User(){ Id=2, Name="Egor", Login="egazan11", Username="agoa", Password=SecurePasswordHasher.Hash("qwerty"), Email="egor@gmail.com", DateCreated=DateTime.Now, IsVerified=false}
            };
            modelBuilder.Entity<User>().HasData(users);

            UserInfo[] userInfo = new UserInfo[]
            {
                new UserInfo() { Id = 1, UserId = 1, Adress="Ukraine", Avatar="/img/avatars/avatar.png", Info="Інформація не надана.", PhoneNumber=""},
                new UserInfo() { Id = 2, UserId = 2, Adress="Croatia", Avatar="/img/avatars/avatar.png", Info="Інформація не надана.", PhoneNumber=""}
            };
            modelBuilder.Entity<UserInfo>().HasData(userInfo);
        }
    }
}