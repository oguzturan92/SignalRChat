using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private IDepartmentDal _departmentDal;
        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        // METOTLAR
        public void Create(Department entity)
        {
            _departmentDal.Create(entity);
        }

        public void Delete(Department entity)
        {
            _departmentDal.Delete(entity);
        }

        public List<Department> GetAll()
        {
            return _departmentDal.GetAll().ToList();
        }

        public Department GetById(int id)
        {
            return _departmentDal.GetById(id);
        }

        public void Update(Department entity)
        {
            _departmentDal.Update(entity);
        }
    }
}