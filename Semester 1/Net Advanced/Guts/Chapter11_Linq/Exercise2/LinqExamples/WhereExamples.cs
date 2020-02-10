using System;
using System.Collections.Generic;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    public class WhereExamples
    {
        public int[] FilterOutNumbersOutOfRange(int[] numbers, int minimum, int maximum)
        {
            return numbers.Where(num => num >= minimum && num <= maximum).ToArray();
        }

        public IList<Person> FilterOutCatLovers(List<Person> persons)
        {
           return persons.Where(person => !person.FavoriteAnimal.ToLower().Equals("cat")).ToList();
        }
    }
}