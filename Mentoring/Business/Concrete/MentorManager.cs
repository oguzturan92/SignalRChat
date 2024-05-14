using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class MentorManager : IMentorService
    {
        private IMentorDal _mentorDal;
        public MentorManager(IMentorDal mentorDal)
        {
            _mentorDal = mentorDal;
        }

        // METOTLAR
        public void Create(Mentor entity)
        {
            _mentorDal.Create(entity);
        }

        public void Delete(Mentor entity)
        {
            _mentorDal.Delete(entity);
        }

        public List<Mentor> GetAll()
        {
            return _mentorDal.GetAll().ToList();
        }

        public Mentor GetById(int id)
        {
            return _mentorDal.GetById(id);
        }

        public Mentor GetMentorAndSocialAndStajByUserId(int userId)
        {
            return _mentorDal.GetMentorAndSocialAndStajByUserId(userId);
        }

        public List<Mentor> GetMentorsAndUsersAndSocial()
        {
            return _mentorDal.GetMentorsAndUsersAndSocial();
        }

        public void Update(Mentor entity)
        {
            _mentorDal.Update(entity);
        }
    }
}