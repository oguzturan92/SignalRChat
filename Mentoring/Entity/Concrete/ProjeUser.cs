using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entity.Concrete
{
    public class ProjeUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public bool IsBlock { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
        public int? MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}