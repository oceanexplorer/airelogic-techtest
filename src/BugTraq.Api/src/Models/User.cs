using System.Collections.Generic;

namespace BugTraq.Api.Models
{
    public class User
    {
        public int UserId { get; set;}
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        public virtual List<Bug> Bugs { get; set; }
    }
}