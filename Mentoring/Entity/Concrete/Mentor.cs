using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Mentor
    {
        [Key]
        public int MentorId { get; set; }
        public string Description { get; set; }
        public bool IsOnline { get; set; }
        public DateTime GraduationYear { get; set; }
        public string Department { get; set; }
        public string Image { get; set; }
        public double Point { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public ProjeUser ProjeUser { get; set; }
        public List<Staj> Stajs { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<Point> Points { get; set; }
    }
}