using ChatterBox.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatterBox.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Box> Boxes { get; set; }
        public DbSet<ChatBox> ChatBoxes { get; set; }
        public DbSet<BoxComment> BoxComments { get; set; }
    }
}
