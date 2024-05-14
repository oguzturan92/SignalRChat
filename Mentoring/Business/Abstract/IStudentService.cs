using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IStudentService
    {
        Student GetById(int id);
        List<Student> GetAll();
        void Create(Student entity);
        void Update(Student entity);
        void Delete(Student entity);
    }
}