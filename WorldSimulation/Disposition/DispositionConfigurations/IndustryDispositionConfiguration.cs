using System;
using System.Collections.Generic;

namespace WorldSimulation.Disposition.DispositionConfigurations
{
    public class IndustryDispositionConfiguration : IDispositionConfiguration
    {
        public string Name { get { return "Industry"; } }
        public IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities)
        {
            throw new NotImplementedException();
        }
    }
}
