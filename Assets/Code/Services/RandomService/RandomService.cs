using System;

namespace Code.Services.RandomService
{
    public class RandomService : IRandomService
    {
        private Random _random;
        
        public RandomService(Random random)
        {
            _random = random;
        }

        public int RandomByNumber(int minValue, int maxValue)
            => _random.Next(minValue, maxValue);
    }
}