using EvaExchange.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaExchange.Data
{
    public class EvaDbContext: DbContext
    {
        public EvaDbContext(DbContextOptions<EvaDbContext> options) : base(options)
        {
        }

        //Tables 
        //public DbSet<tableName> Employees {get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Share>(entity =>
            {
                entity.HasIndex(s => s.Symbol).IsUnique();
                entity.Property(s => s.Symbol).HasConversion(s => s.ToUpper(), s => s);
                entity.Property(s => s.Price).HasColumnType("decimal(18,2)");
                entity.Property(s => s.Symbol).HasMaxLength(3);
                entity.Property(s => s.PriceChangeDate).HasDefaultValue(DateTime.UtcNow);
                //entity.Property(s => s.Quantity)
            });
            modelBuilder.Entity<Trade>(entity =>
            {
                entity.Property(t => t.TradeTime).HasDefaultValue(DateTime.UtcNow);
                entity.Property(t => t.TradeType).HasConversion<string>();
            });

            //Seed Data
            modelBuilder.Entity<User>().HasData(
                new User() { Id=1,Name= "Jesse Livermore" },
                new User() { Id=2, Name = "William Delbert Gann" },
                new User() { Id=3, Name = "George Soros" },
                new User() { Id=4, Name = "Richard Dennis" },
                new User() { Id=5, Name = "Paul Tudor Jones" });

            modelBuilder.Entity<Share>().HasData(
                new Share() { Id = 1, Symbol = "EA", Price = 13, Quantity = 50 },
                new Share() { Id = 2, Symbol = "PEP", Price = 88, Quantity = 300 },
                new Share() { Id = 3, Symbol = "ROP", Price = 26, Quantity = 80 },
                new Share() { Id = 4, Symbol = "TXN", Price = 35, Quantity = 100 },
                new Share() { Id = 5, Symbol = "GLD", Price = 60, Quantity = 70 });

            modelBuilder.Entity<Portfolio>().HasData
                (new Portfolio() { Id = 1, TotalAmount = 2000, UserId = 1 }
            );
            modelBuilder.Entity<Trade>().Property(t => t.Id).ValueGeneratedOnAdd();
            
        }
    }
}
