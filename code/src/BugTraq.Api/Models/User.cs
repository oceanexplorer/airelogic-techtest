using System;
using System.Collections.Generic;

namespace BugTraq.Api.Models
{
    public class User
    {
        public User(Guid userId, string firstName, string surname)
        {
            UserId = userId;
            FirstName = firstName;
            Surname = surname;
        }
        
        public User(string firstName, string surname)
        {
            UserId = Guid.NewGuid();
            FirstName = firstName;
            Surname = surname;
        }
        
        public Guid UserId { get; set;}
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        public virtual List<Bug> Bugs { get; set; }
    }
}