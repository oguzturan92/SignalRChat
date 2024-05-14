using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IMentorService
    {
        Mentor GetById(int id);
        List<Mentor> GetAll();
        void Create(Mentor entity);
        void Update(Mentor entity);
        void Delete(Mentor entity);
        Mentor GetMentorAndSocialAndStajByUserId(int userId);
        List<Mentor> GetMentorsAndUsersAndSocial();
    }
}