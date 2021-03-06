using System.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace StorageApp.Entities
{
     public class StorageContext : DbContext, IStorageContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Container> Container { get; set; }
        public DbSet<UserContainer> UserContainer { get; set; }
        public DbSet<Item> Item { get; set; }

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasKey(u => u.Id);

            modelBuilder.Entity<Container>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<Item>()
                        .HasKey(i => i.Id);
            
            modelBuilder.Entity<UserContainer>()
                        .HasKey(uc => new {uc.UserId, uc.ContainerId});

            modelBuilder.Entity<UserContainer>()
                        .HasOne(uc => uc.User)
                        .WithMany(u => u.UserContainers)
                        .HasForeignKey(uc => uc.UserId);
            
            modelBuilder.Entity<UserContainer>()
                        .HasOne(uc => uc.Container)
                        .WithMany(c => c.UserContainers)
                        .HasForeignKey(uc => uc.ContainerId);

            modelBuilder.Entity<Item>()
                        .HasOne<Container>(i => i.Container)
                        .WithMany(c => c.Items)
                        .HasForeignKey(i => i.ContainerId);
        }
    }
}