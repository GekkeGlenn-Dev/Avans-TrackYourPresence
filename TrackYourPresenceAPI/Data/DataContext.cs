using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure one-to-many relationship
            // modelBuilder.Entity<Viewing>()
            //     .HasOne<Movie>(v => v.Movie);
            //
            // modelBuilder.Entity<Viewing>()
            //     .HasOne<Theatre>(v => v.Theatre);
            //
            // modelBuilder.Entity<Viewing>()
            //     .HasMany<ViewingSeat>(v => v.ViewingSeats);
        }

        public DbSet<User> Users { get; set; }
    }
}