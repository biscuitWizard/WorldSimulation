using System.Collections.Generic;

namespace WorldSimulation.Disposition
{
    public interface IDispositionConfiguration
    {
        string Name { get; }
        IList<Proclivity> GenerateDefaultProclivites(IList<Proclivity> proclivities);
    }
}