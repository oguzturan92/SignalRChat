using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IStajService
    {
        Staj GetById(int id);
        List<Staj> GetAll();
        void Create(Staj entity);
        void Update(Staj entity);
        void Delete(Staj entity);
    }
}