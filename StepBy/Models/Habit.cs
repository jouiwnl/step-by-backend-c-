using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("habits")]
    public class Habit
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String title { get; set; }
        public DateTime created_at { get; set; }
        public String user_id { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }

        [InverseProperty(nameof(DayHabit.habit))]
        public List<DayHabit> dayHabits { get; set; }

        [InverseProperty(nameof(HabitWeekDays.habit))]
        public List<HabitWeekDays> weekDays { get; set; }
    }
}
