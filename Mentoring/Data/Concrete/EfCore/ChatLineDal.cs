using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class ChatLineDal : GenericDal<ChatLine>, IChatLineDal
    {
        
    }
}