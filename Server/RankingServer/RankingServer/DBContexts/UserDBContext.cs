using Microsoft.EntityFrameworkCore;
using RankingServer.Models;

namespace RankingServer.DBContexts;

public class UserDBContext : DbContext
{
    public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }  
}