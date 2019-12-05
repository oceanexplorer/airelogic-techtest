using System;

namespace BugTraq.Api.Models
{
    public class Bug
    {
        public Bug()
        {
            Status = "Open";
            CreatedDate = DateTime.UtcNow;
        }

        public Bug(string title, string description, string status, int userId)
        {
            Title = title;
            Description = description;
            Status = status;
            UserId = userId;
            CreatedDate = DateTime.UtcNow;
        }
        
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } 

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}