using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DavidoffBot.Models
{
    public class BotContext : DbContext
    {
        private IConfiguration _configuration;
        public BotContext() { Console.WriteLine("empty"); }

        public BotContext(DbContextOptions<BotContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Console.WriteLine("options");
        }

        public DbSet<BotMessage> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<BotMessageUser> BotMessageUsers { get; set; }
        public DbSet<BotMessageKeyword> BotMessageKeywords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var startPath = Environment.CurrentDirectory.Split("bin").First();
            //var endPath = _configuration.GetConnectionString("SQLDbStorage");
            Console.WriteLine(startPath);
            //Console.WriteLine(endPath);

            optionsBuilder.UseSqlite($"Data Source={startPath}\\App_Data\\Data_bot.sdf;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotMessageUser>()
                .HasKey(bmu => new { bmu.UserId, bmu.MessageId });

            modelBuilder.Entity<BotMessageUser>()
                .HasOne(bmu => bmu.User)
                .WithMany(u => u.BotMessageUsers)
                .HasForeignKey(bmu => bmu.UserId);

            modelBuilder.Entity<BotMessageUser>()
                .HasOne(bmu => bmu.Message)
                .WithMany(u => u.BotMessageUsers)
                .HasForeignKey(bmu => bmu.MessageId);

            modelBuilder.Entity<BotMessageKeyword>()
                .HasKey(bmk => new { bmk.KeywordId, bmk.BotMessageId });

            modelBuilder.Entity<BotMessageKeyword>()
                .HasOne(bmk => bmk.BotMessage)
                .WithMany(u => u.BotMessageKeywords)
                .HasForeignKey(bmk => bmk.BotMessageId);

            modelBuilder.Entity<BotMessageKeyword>()
                .HasOne(bmk => bmk.Keyword)
                .WithMany(u => u.BotMessageKeywords)
                .HasForeignKey(bmk => bmk.KeywordId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.TelegramId);

            modelBuilder.Entity<Keyword>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<BotMessage>()
                .HasKey(u => u.Id);
        }
    }
}
