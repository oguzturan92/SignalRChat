using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IChatLineService
    {
        ChatLine GetById(int id);
        List<ChatLine> GetAll();
        void Create(ChatLine entity);
        void Update(ChatLine entity);
        void Delete(ChatLine entity);
    }
}