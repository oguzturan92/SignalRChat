using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class MentorIndexModel
    {
        public ProjeUser User { get; set; }
        public Mentor Mentor { get; set; }
    }
}