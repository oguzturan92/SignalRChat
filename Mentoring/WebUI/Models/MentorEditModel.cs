using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class MentorEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public int MentorId { get; set; }
        public DateTime GraduationYear { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Department> Departments { get; set; }
    }
}