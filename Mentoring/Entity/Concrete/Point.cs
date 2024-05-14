using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Point
    {
        public int PointId { get; set; }
        public int PointNo { get; set; }
        public int PointScorerUserId { get; set; }
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}