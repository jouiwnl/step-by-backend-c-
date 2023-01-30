using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StepBy.Models
{
    [Table("years")]
    public class Year
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public int year_number { get; set; }
        public String user_id { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }
    }
}
