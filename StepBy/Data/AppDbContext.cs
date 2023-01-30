using Microsoft.EntityFrameworkCore;
using StepBy.Models;

namespace StepBy.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Year> years { get; set; }
        public DbSet<DayHabit> dayHabits { get; set; }
        public DbSet<Day> days { get; set; }
        public DbSet<Habit> habits { get; set; }
        public DbSet<HabitWeekDays> habitWeekDays { get; set; }
        public DbSet<NotificationToken> notificationTokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = db.eizpeurodmkyqfliupfk.supabase.co; Database = postgres; Username = postgres; Password = 132Natan123!");
        }
    }
}
