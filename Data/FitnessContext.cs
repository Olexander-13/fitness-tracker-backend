using Microsoft.EntityFrameworkCore;
using FitnessTrackerApi.Models;

namespace FitnessTrackerApi.Data
{
    public class FitnessContext : DbContext
    {
        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}