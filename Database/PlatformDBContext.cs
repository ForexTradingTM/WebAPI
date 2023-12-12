using ForexTradingBotWebAPI.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace ForexTradingBotWebAPI.Database;

public class PlatformDBContext : DbContext
{
    public PlatformDBContext(DbContextOptions<PlatformDBContext> options)
        : base(options)
    {
    }

    // DbSet for other entities from the Web API project
    public DbSet<User> Users { get; set; }
}
