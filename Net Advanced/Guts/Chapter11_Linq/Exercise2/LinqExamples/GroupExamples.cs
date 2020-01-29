using System;
using System.Collections.Generic;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    public class GroupExamples
    {

        public IList<IGrouping<bool, int>> GroupSmallAndBigNumbers(int[] numbers)
        {
            return numbers.GroupBy(number => number <= 100).ToList();
        }

        public IList<PersonAgeGroup> GroupPersonsByAge(List<Person> persons)
        {
            return persons.GroupBy(p => p.Age).Select(g => new PersonAgeGroup {
                Age = g.Key,
                Persons = g.ToList()
            }).ToList();
        }
    }
}