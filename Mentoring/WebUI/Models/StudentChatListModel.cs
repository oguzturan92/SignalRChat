using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class StudentChatListModel
    {
        public int ChatId { get; set; }
        public string ChatImage { get; set; }
        public string ChatFullname { get; set; }
        public bool ChatRead { get; set; }

        public DateTime ChatDate { get; set; }
        public string ChatLineLast { get; set; }
    }
}