using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentImage { get; set; }
        public DateTime StartingYear { get; set; }
        public int Id { get; set; }
        public ProjeUser ProjeUser { get; set; }
    }
}