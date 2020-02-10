using Lottery.Business.Interfaces;
using Lottery.Data.Interfaces;
using Lottery.Domain;
using System;
using System.Collections.Generic;

namespace Lottery.Business
{
    public class DrawService : IDrawService
    {
        IDrawRepository _drawRepository;
        Random random = new Random();
        public DrawService(IDrawRepository drawRepository)
        {
            _drawRepository = drawRepository;
        }

        public void CreateDrawFor(LotteryGame lotteryGame)
        {
            IList<int> numbers = new List<int>();
            int min = 1;
            int max = lotteryGame.MaximumNumber + 1;

            int num = random.Next(min, max);
            numbers.Add(num);
            for (int i = 1; i < lotteryGame.NumberOfNumbersInADraw; i++)
            {
                num = random.Next(min, max);
                while (numbers.Contains(num))
                {
                    num = random.Next(min, max);

                }
                numbers.Add(num);
            }
            _drawRepository.Add(lotteryGame.Id, numbers);
        }
    }
}
