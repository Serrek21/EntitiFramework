using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;
using Match = DAL.Entities.Match;

namespace EntityFramework
{
    public class AppDBcontext : DbContext
    {
        public DbSet<FootballTeam> FootballTeams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<GoalDetail> GoalDetails { get; set; }
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options) { }

        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<FootballTeam>()
        //        .HasMany(t => t.MatchesAsTeam1)
        //        .WithOne(m => m.Team1)  
        //        .HasForeignKey(m => m.Team1Id)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<FootballTeam>()
        //        .HasMany(t => t.MatchesAsTeam2)
        //        .WithOne(m => m.Team2)
        //        .HasForeignKey(m => m.Team2Id)
        //        .OnDelete(DeleteBehavior.Restrict); 
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Football;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

    }
}