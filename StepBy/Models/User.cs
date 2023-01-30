using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("users")]
    public class User
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String email { get; set; }

        public ICollection<Year> years { get; set; }
        public ICollection<Habit> habits { get; set; }
        public ICollection<Day> days { get; set; }
    }
}
