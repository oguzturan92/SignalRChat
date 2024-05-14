using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private IStudentDal _studentDal;
        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        // METOTLAR
        public void Create(Student entity)
        {
            _studentDal.Create(entity);
        }

        public void Delete(Student entity)
        {
            _studentDal.Delete(entity);
        }

        public List<Student> GetAll()
        {
            return _studentDal.GetAll().ToList();
        }

        public Student GetById(int id)
        {
            return _studentDal.GetById(id);
        }

        public void Update(Student entity)
        {
            _studentDal.Update(entity);
        }
    }
}