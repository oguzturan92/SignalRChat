using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class MentorDal : GenericDal<Mentor>, IMentorDal
    {
        public Mentor GetMentorAndSocialAndStajByUserId(int userId)
        {
            using (var context = new Context())
            {
                return context.Mentors
                                    .Where(i => i.Id == userId)
                                    .Include(i => i.SocialMedias.OrderBy(i => i.SocialMediaRowNumber))
                                    .Include(i => i.Stajs.OrderBy(i => i.StajRowNumber))
                                    .Include(i => i.Points)
                                    .FirstOrDefault();
            }
        }

        public List<Mentor> GetMentorsAndUsersAndSocial()
        {
            using (var context = new Context())
            {
                return context.Mentors
                                    .Include(i => i.ProjeUser)
                                    .Include(i => i.SocialMedias.OrderBy(i => i.SocialMediaRowNumber))
                                    .Include(i => i.Points)
                                    .ToList();
            }
        }
    }
}