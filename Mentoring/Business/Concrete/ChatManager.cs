using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class ChatManager : IChatService
    {
        private IChatDal _chatDal;
        public ChatManager(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        // METOTLAR
        public void Create(Chat entity)
        {
            _chatDal.Create(entity);
        }

        public void Delete(Chat entity)
        {
            _chatDal.Delete(entity);
        }

        public List<Chat> GetAll()
        {
            return _chatDal.GetAll().ToList();
        }

        public Chat GetById(int id)
        {
            return _chatDal.GetById(id);
        }

        public List<Chat> GetChatsAndChatLineLast(int baseUserId)
        {
            return _chatDal.GetChatsAndChatLineLast(baseUserId);
        }

        public void Update(Chat entity)
        {
            _chatDal.Update(entity);
        }
    }
}