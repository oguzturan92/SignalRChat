using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IPointService
    {
        Point GetById(int id);
        List<Point> GetAll();
        void Create(Point entity);
        void Update(Point entity);
        void Delete(Point entity);
    }
}