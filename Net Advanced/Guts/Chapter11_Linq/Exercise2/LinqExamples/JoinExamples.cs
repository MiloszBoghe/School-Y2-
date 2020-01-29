using System;
using System.Collections.Generic;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    public class JoinExamples
    {
        public int[] GetIntersection(int[] firstList, int[] secondList)
        {
            return firstList.Join(secondList, n1 => n1, n2 => n2, (n1, n2) => n1).ToArray();
        }

        public IList<string> FindCouplesByFavoriteAnimalUsingJoin(List<Person> boys, List<Person> girls)
        {
            return boys.Join(girls, b => b.FavoriteAnimal, g => g.FavoriteAnimal, (b, g) => b.Firstname + " and " + g.Firstname).ToList();
        }
    }
}