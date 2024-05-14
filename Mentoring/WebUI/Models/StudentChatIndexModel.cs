using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebUI.Models
{
    public class StudentChatIndexModel
    {
        public int FocusChatId { get; set; }
        public int BaseUserId { get; set; }
        public List<StudentChatListModel> StudentChatList { get; set; }
        public List<ChatLine> ChatLines { get; set; }
        public int NewMessageReceiverUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public string UserFullname { get; set; }
    }
}