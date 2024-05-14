using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class StudentIndexModel
    {
        public List<Mentor> Mentors { get; set; }
        public List<Department> Departments { get; set; }
    }
}