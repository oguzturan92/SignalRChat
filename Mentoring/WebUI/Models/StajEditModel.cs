using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StajEditModel
    {
        public int StajId { get; set; }
        public string StajTitle { get; set; }
        public string StajSubTitle { get; set; }
        public int StajRowNumber { get; set; }
        public int MentorId { get; set; }
    }
}