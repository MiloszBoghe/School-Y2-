using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDemo.Data;
using EFDemo.Domain;

namespace EFDemo.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertSamurai();




            Console.Write("\nPress return to quit...");
            Console.ReadLine();
        }

        private static void InsertSamurai()
        {
            Samurai samurai = new Samurai
            {
                Name = "Samurai1",
            };

            using (SamuraiContext samuraiContext = new SamuraiContext())
            {
                samuraiContext.Samurais.Add(samurai);
                samuraiContext.SaveChanges();
            }
        }
    }
}
