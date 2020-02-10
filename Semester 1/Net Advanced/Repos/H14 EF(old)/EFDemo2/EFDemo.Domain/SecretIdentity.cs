using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Domain
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // beide hoeft niet, maar makkelijker aan Samurai komen
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
    }
}
