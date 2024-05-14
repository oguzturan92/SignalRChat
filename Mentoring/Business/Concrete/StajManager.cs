using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class StajManager : IStajService
    {
        private IStajDal _stajDal;
        public StajManager(IStajDal stajDal)
        {
            _stajDal = stajDal;
        }

        // METOTLAR
        public void Create(Staj entity)
        {
            _stajDal.Create(entity);
        }

        public void Delete(Staj entity)
        {
            _stajDal.Delete(entity);
        }

        public List<Staj> GetAll()
        {
            return _stajDal.GetAll().ToList();
        }

        public Staj GetById(int id)
        {
            return _stajDal.GetById(id);
        }

        public void Update(Staj entity)
        {
            _stajDal.Update(entity);
        }
    }
}