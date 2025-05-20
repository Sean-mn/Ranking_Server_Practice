using Microsoft.EntityFrameworkCore;
using RankingServer.Models;

namespace RankingServer.DBContexts;

public class RankDBContext : DbContext
{
    public DbSet<UserRank> UserRanks { get; set; }
    
    public RankDBContext(DbContextOptions<RankDBContext> options) : base(options) { }
}