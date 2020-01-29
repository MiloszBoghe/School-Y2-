using System;
using System.Collections.Generic;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    public class OrderByExamples
    {
        public string[] SortWordsDescending(string[] words)
        {
            return words.OrderByDescending(word => word).ToArray();
        }

        public IList<Person> SortPersonsOnDescendingAgeAndThenOnFavoriteAnimalAscending(List<Person> persons)
        {
            return persons.OrderByDescending(person => person.Age).ThenBy(person=>person.FavoriteAnimal).ToList();
        }
    }
}