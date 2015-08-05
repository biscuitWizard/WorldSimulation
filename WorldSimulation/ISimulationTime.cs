using System;

namespace WorldSimulation
{
    /// <summary>
    /// Represents the time inside of a simulation.
    /// </summary>
    public interface ISimulationTime
    {
        DateTime CurrentTime { get; }

        DateTime AdvanceTick();
    }
}
