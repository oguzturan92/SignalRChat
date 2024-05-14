using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class ChatLine
    {
        public int ChatLineId { get; set; }
        public string ChatLineMessage { get; set; }
        public DateTime ChatLineDate { get; set; }
        public int ChatLineSenderUserId { get; set; }
        public int ChatLineReceiverUserId { get; set; }
        public bool Document { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}