using System;
using System.Collections.Generic;
using StepBy.Models;

namespace StepBy.Representation
{
    public interface HabitRepresentation
    {

        public class ListRepresentation
        {
            public String id { get; set; } = Guid.NewGuid().ToString();
            public String title { get; set; }
            public DateTime created_at { get; set; }
            public String user_id { get; set; }

            public String weekDays { get; set; }

            public static ListRepresentation of(Habit habit)
            {
                return new ListRepresentation
                {
                    id = habit.id,
                    title = habit.title,
                    created_at = habit.created_at,
                    user_id = habit.user_id,
                    weekDays = String.Join(",", habit.weekDays.ConvertAll(w => w.week_day))
                };
            }
        }

        public class OneRepresentation
        {
            public String id { get; set; } = Guid.NewGuid().ToString();
            public String title { get; set; }
            public DateTime created_at { get; set; }
            public String user_id { get; set; }

            public List<WeekDayAssociation> weekDays { get; set; }

            public static OneRepresentation of(Habit habit)
            {
                return new OneRepresentation
                {
                    id = habit.id,
                    title = habit.title,
                    created_at = habit.created_at,
                    user_id = habit.user_id,
                    weekDays = habit.weekDays.ConvertAll(w => WeekDayAssociation.of(w))
                };
            }
        }

        public class WeekDayAssociation
        {
            public String id { get; set; } = Guid.NewGuid().ToString();
            public String habit_id { get; set; }
            public int week_day { get; set; }

            public static WeekDayAssociation of(HabitWeekDays weekDay)
            {
                return new WeekDayAssociation
                {
                    id = weekDay.id,
                    habit_id = weekDay.habit_id,
                    week_day = weekDay.week_day
                };
            }
        }
    }
}
