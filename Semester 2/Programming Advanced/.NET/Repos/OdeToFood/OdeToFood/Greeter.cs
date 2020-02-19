using System;

namespace OdeToFood
{
    internal class Greeter : IGreeter
    {
        public string GetMessageOfTheDay()
        {
            return $"Welcome on this lovely {DateTime.Today.DayOfWeek}";
         }
    }
}