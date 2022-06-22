using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackYourPresence.Models;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<AbsentItem> AbsentItems { get; set; }
        public DbSet<LeaveOfAbsence> LeaveOfAbsences { get; set; }
    }
}