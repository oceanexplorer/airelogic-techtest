using System;
using System.Collections.Generic;

namespace BugTraq.Api.Models
{
    public class User
    {
        public Guid UserId { get; set;}
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        public virtual List<Bug> Bugs { get; set; }
    }
}