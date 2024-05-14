using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
        Department GetById(int id);
        List<Department> GetAll();
        void Create(Department entity);
        void Update(Department entity);
        void Delete(Department entity);
    }
}