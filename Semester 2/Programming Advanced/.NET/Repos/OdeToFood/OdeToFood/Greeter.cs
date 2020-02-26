using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
    internal class Greeter : IGreeter
    {
        private readonly IConfiguration _configuration;

        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetMessageOfTheDay()
        {
            return _configuration["Greeting"];
        }
    }
}
