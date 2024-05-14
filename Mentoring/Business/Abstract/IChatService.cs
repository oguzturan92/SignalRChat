using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IChatService
    {
        Chat GetById(int id);
        List<Chat> GetAll();
        void Create(Chat entity);
        void Update(Chat entity);
        void Delete(Chat entity);
        List<Chat> GetChatsAndChatLineLast(int baseUserId);
    }
}