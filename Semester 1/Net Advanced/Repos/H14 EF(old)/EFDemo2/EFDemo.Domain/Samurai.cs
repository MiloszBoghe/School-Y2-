using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            Quotes = new List<Quote>();
            SamuraiBattles = new List<SamuraiBattle>();
        }

        
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Quote> Quotes { get; set; }

        //public int BattleId { get; set; }
        //public Battle Battle { get; set; }

        public List<SamuraiBattle> SamuraiBattles { get; set; }

        //public int SecretIdentityId { get; set; }
        public SecretIdentity SecretIdentity { get; set; }

    }
}
