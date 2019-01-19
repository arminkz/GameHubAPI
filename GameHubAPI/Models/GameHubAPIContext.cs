using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameHubAPI.Models
{
    public class GameHubAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GameHubAPIContext() : base("name=GameHubAPIContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Friendship>().HasKey(f => new { f.UserId1, f.UserId2 });
                    modelBuilder.Entity<Friendship>()
                        .HasRequired(f => f.User1)
                        .WithMany()
                        .HasForeignKey(f => f.UserId1);
                    modelBuilder.Entity<Friendship>()
                        .HasRequired(f => f.User2)
                        .WithMany()
                        .HasForeignKey(f => f.UserId2)
                        .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<GameHubAPI.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<GameHubAPI.Models.Game> Games { get; set; }
        public System.Data.Entity.DbSet<GameHubAPI.Models.Friendship> Friendships { get; set; }
    }
}
