using DreamNDine.BLL.Models;
using Microsoft.EntityFrameworkCore;


namespace DreamNDine.BLL.DbContext
{
    public class DreamNDineContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
        public DreamNDineContext(DbContextOptions<DreamNDineContext> options)
            : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SILVERBACK\\SQLEXPRESS;Database=DreamNDine;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
        }
    }
}
