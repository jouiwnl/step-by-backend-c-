using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("days")]
    public class Day
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public DateTime date { get; set; }
        public String user_id { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }
    }
}
