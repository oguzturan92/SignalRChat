using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class ChatLineManager : IChatLineService
    {
        private IChatLineDal _chatLineDal;
        public ChatLineManager(IChatLineDal chatLineDal)
        {
            _chatLineDal = chatLineDal;
        }

        // METOTLAR
        public void Create(ChatLine entity)
        {
            _chatLineDal.Create(entity);
        }

        public void Delete(ChatLine entity)
        {
            _chatLineDal.Delete(entity);
        }

        public List<ChatLine> GetAll()
        {
            return _chatLineDal.GetAll().ToList();
        }

        public ChatLine GetById(int id)
        {
            return _chatLineDal.GetById(id);
        }

        public void Update(ChatLine entity)
        {
            _chatLineDal.Update(entity);
        }
    }
}