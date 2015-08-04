using System;
using System.Collections.Generic;

namespace WorldSimulation.Disposition
{
    public class DispositionArchitecture
    {
        private readonly IDispositionConfiguration _configuration;

        private readonly List<Proclivity> _proclivities = new List<Proclivity>();

        public DispositionArchitecture(IDispositionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Category
        {
            get { return _configuration.Name; }
        }

        public void Randomize()
        {
            var random = new Random();

            foreach (var proclivity in _proclivities)
            {
                proclivity.IncreaseIntensity(random.Next(0, 10));
                if (random.Next(0, 2) == 1)
                {
                    proclivity.IncrementRight(random.Next(0, 15));
                }
                else
                {
                    proclivity.IncrementLeft(random.Next(0, 15));
                }
            }
        }

        public void Initialize()
        {
            _proclivities.AddRange(_configuration.GenerateDefaultProclivites(_proclivities));
        }

        public Proclivity[] GetProclivities()
        {
            return _proclivities.ToArray();
        }
    }
}