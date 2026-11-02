using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace AdminPanel
{
    internal class FitnessContext : DbContext
    {
        const string CONN_STRING = "server=localhost;port=3306;database=fitness;uid=root;pwd=;";

        public DbSet<User> Users { get; set; }
        public DbSet<Allergene> Allergenes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UserAllergies> UserAllergies { get; set; }
        public DbSet<ItemAllergenes> ItemAllergenes { get; set; }

        // AI generated
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Allergies)
                .WithMany(a => a.Users)
                .UsingEntity<UserAllergies>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using var conn = new MySqlConnection(CONN_STRING);
            optionsBuilder.UseMySql(CONN_STRING, ServerVersion.AutoDetect(conn));
        }
    }

    [Table("users")]
    internal class User
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("gender")]
        public int Gender { get; set; }
        [Column("weight")]
        public double Weight { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("height")]
        public double Height { get; set; }
        [Column("score")]
        public int Score { get; set; }
        [Column("streak")]
        public int Streak { get; set; }

        public ICollection<Allergene> Allergies { get; set; } = [];
    }

    [Table("allergenes")]
    internal class Allergene
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = [];
    }

    [Table("user_allergies"), PrimaryKey(nameof(UserId), nameof(AllergeneId))]
    internal class UserAllergies
    {
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("allergene_id")]
        public int AllergeneId { get; set; }

        public User User { get; set; } = new User();
        public Allergene Allergene { get; set; } = new Allergene();
    }

    [Table("items")]
    internal class Item
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("calories")]
        public double Calories { get; set; }
        [Column("carbohydrates")]
        public double Carbohydrates { get; set; }
        [Column("fat")]
        public double Fat { get; set; }
        [Column("protein")]
        public double Protein { get; set; }
    }

    [Table("item_allergenes"), PrimaryKey(nameof(ItemId), nameof(AllergeneId))]
    internal class ItemAllergenes
    {
        [Column("item_id")]
        public int ItemId { get; set; }
        [Column("allergene_id")]
        public int AllergeneId { get; set; }

        public Item Item { get; set; } = new Item();
        public Allergene Allergene { get; set; } = new Allergene();
    }
}
