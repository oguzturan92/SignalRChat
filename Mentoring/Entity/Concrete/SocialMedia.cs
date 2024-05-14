using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string SocialMediaTitle { get; set; }
        public string SocialMediaIcon { get; set; }
        public string SocialMediaLink { get; set; }
        public int SocialMediaRowNumber { get; set; }
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}