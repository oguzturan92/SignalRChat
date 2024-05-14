using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StudentEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentImage { get; set; }
        public bool Gender { get; set; }
        public DateTime StartingYear { get; set; }
    }
}