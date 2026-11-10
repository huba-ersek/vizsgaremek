using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel
{
    internal static class DatabaseConstants
    {
        public const string USERS_TABLE = "users";
        public const string ALLERGENES_TABLE = "allergenes";
        public const string USER_ALLERGIES_TABLE = "user_allergies";
        public const string ITEMS_TABLE = "items";
        public const string ITEM_ALLERGENES_TABLE = "item_allergenes";
    }
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
            try
            {
                using var conn = new MySqlConnection(CONN_STRING);
                optionsBuilder.UseMySql(CONN_STRING, ServerVersion.AutoDetect(conn));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Hiba az adatbázishoz csatlakozás közben:\n\n{ex.Message}\n\nAz OK gombra kattintva a program kilép!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(-1);
            }
        }
    }

    [Table(DatabaseConstants.USERS_TABLE)]
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

    [Table(DatabaseConstants.ALLERGENES_TABLE)]
    internal class Allergene
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = [];
    }

    [Table(DatabaseConstants.USER_ALLERGIES_TABLE), PrimaryKey(nameof(UserId), nameof(AllergeneId))]
    internal class UserAllergies
    {
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("allergene_id")]
        public int AllergeneId { get; set; }

        public User User { get; set; } = new User();
        public Allergene Allergene { get; set; } = new Allergene();
    }

    [Table(DatabaseConstants.ITEMS_TABLE)]
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

    [Table(DatabaseConstants.ITEM_ALLERGENES_TABLE), PrimaryKey(nameof(ItemId), nameof(AllergeneId))]
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
