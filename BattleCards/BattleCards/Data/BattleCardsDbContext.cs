namespace BattleCards.Data
{
    using BattleCards.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BattleCardsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>()
                .HasKey(k => new { k.CardId, k.UserId });
        }
    }
}
