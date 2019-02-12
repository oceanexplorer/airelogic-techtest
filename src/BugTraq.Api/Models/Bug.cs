using System;

namespace BugTraq.Api.Models
{
    public class Bug
    {
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } 

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}