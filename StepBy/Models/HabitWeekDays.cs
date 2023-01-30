using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("habit_week_days")]
    public class HabitWeekDays
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String habit_id { get; set; }
        public int week_day { get; set; }

        [ForeignKey("habit_id")]
        public Habit habit { get; set; }
    }
}
