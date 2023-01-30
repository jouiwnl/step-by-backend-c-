using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("day_habits")]
    public class DayHabit
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String day_id { get; set; }
        public String habit_id { get; set; }

        [ForeignKey("day_id")]
        public Day day { get; set; }

        [ForeignKey("habit_id")]
        public Habit habit { get; set; }
    }
}
