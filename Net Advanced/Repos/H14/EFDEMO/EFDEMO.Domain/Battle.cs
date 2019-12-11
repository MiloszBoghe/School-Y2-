using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDEMO.Domain
{
    public class Battle
    {

        public Battle()
        {
            Samurais = new List<Samurai>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Samurai> Samurais { get; set; }


    }
}
