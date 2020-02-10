using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    public class SelectExamples
    {
        //Tip: use the "ToList" extension method to convert an IEnumerable to a List

        public IList<string> ConvertWordsToUpper(IEnumerable<string> words)
        {
            return words.Select(word => word.ToUpper()).ToList();
        }

        public IList<PersonSummary> ConvertPersonsToPersonSummaries(IEnumerable<Person> persons)
        {
            return persons.Select(person =>
            new PersonSummary
            {
                FullName = person.Firstname + " " + person.Lastname,
                IsAdult = person.Age >= 18
            }).ToList();
        }
    }
}
