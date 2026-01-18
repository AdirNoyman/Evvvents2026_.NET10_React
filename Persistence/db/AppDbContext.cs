using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.db
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Activity> Activities { get; set; }
    }
}