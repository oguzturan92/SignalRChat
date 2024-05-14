using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int ChatSenderUserId { get; set; }
        public int ChatReceiverUserId { get; set; }
        public bool ChatRead { get; set; }
        public DateTime ChatStartingDate { get; set; }
        public List<ChatLine> ChatLines { get; set; }
    }
}