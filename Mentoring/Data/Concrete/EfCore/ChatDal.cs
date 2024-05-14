using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class ChatDal : GenericDal<Chat>, IChatDal
    {
        public List<Chat> GetChatsAndChatLineLast(int baseUserId)
        {
            using (var context = new Context())
            {
                return context.Chats
                                .Where(i => i.ChatSenderUserId == baseUserId || i.ChatReceiverUserId == baseUserId)
                                .OrderByDescending(i => i.ChatStartingDate)
                                .Include(i => i.ChatLines.OrderByDescending(i => i.ChatLineDate).Take(1))
                                .ToList();
            }
        }
    }
}