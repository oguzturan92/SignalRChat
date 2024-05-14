using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ISocialMediaService
    {
        SocialMedia GetById(int id);
        List<SocialMedia> GetAll();
        void Create(SocialMedia entity);
        void Update(SocialMedia entity);
        void Delete(SocialMedia entity);
    }
}