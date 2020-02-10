using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Domain
{
    public class SamuraiBattle
    {
        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }

        public Battle Battle { get; set; }
        public int BattleId { get; set; }
    }
}
