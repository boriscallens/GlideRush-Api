using GlideRush.Leaderboard.Domain;

using Microsoft.EntityFrameworkCore;

namespace GlideRush.Leaderboard.Persistence;

public class LeaderboardContext : DbContext
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardEntry> BoardEntries { get; set; }

    public LeaderboardContext(DbContextOptions options, ISqlConnectionFactory connectionFactory) : base(options)
    {
        _connectionFactory = connectionFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _connectionFactory != null)
        {
            optionsBuilder.UseSqlServer(_connectionFactory.CreateConnection());
        }
    }


    // TODO: any indexes or field properties that can't be deduced from c# model go here
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);
    //}
}