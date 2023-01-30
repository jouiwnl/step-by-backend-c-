using System;
namespace StepBy.Models
{
    public class NotificationToken
    {
        public String id { get; set; } = Guid.NewGuid().ToString();
        public String token { get; set; }
        public String user_id { get; set; }
    }
}
