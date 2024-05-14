using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class MentorDetailModel
    {
        public ProjeUser User { get; set; }
        public Mentor Mentor { get; set; }
        public decimal Point { get; set; }
    }
}