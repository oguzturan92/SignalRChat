using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class PointManager : IPointService
    {
        private IPointDal _pointDal;
        public PointManager(IPointDal pointDal)
        {
            _pointDal = pointDal;
        }

        // METOTLAR
        public void Create(Point entity)
        {
            _pointDal.Create(entity);
        }

        public void Delete(Point entity)
        {
            _pointDal.Delete(entity);
        }

        public List<Point> GetAll()
        {
            return _pointDal.GetAll().ToList();
        }

        public Point GetById(int id)
        {
            return _pointDal.GetById(id);
        }

        public void Update(Point entity)
        {
            _pointDal.Update(entity);
        }
    }
}