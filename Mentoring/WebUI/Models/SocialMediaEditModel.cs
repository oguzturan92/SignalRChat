using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class SocialMediaEditModel
    {
        public string SocialMediaTitle { get; set; }
        public int SocialMediaId { get; set; }
        public string SocialMediaIcon { get; set; }
        public string SocialMediaLink { get; set; }
        public int SocialMediaRowNumber { get; set; }
        public int MentorId { get; set; }
    }
}