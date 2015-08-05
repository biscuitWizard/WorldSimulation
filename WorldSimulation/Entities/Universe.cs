using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WorldSimulation.Worlds;

namespace WorldSimulation.Entities
{
    /// <summary>
    /// Represents the universe from which the simulation is taking place.
    /// 
    /// There can be many universes for separate simulations, 
    /// and that is called a multiverse.
    /// </summary>
    public class Universe : DataEntity
    {
        public static Universe CurrentUniverse;

        public DateTime CurrentTime { get; private set; }
        public Random RandomGenerator { get; private set; }

        private readonly IList<IModule> _modules = new List<IModule>();
        private readonly ILifeCycle _universeLifeCycle;
        private readonly ISimulationTime _simulationTime;

        protected Universe(ILifeCycle universeLifeCycle, ISimulationTime simulationTime)
        {
            _universeLifeCycle = universeLifeCycle;
            _simulationTime = simulationTime;
        }

        public static Universe Create(ILifeCycle lifeCycle, ISimulationTime simulationTime, IEnumerable<IModule> modules = null, Random random = null)
        {
            var universe = new Universe(lifeCycle, simulationTime) { RandomGenerator = random ?? new Random() };

            // TODO: Manage CurrentUniverses.
            CurrentUniverse = universe;

            if (modules != null)
            {
                foreach (var module in modules)
                {
                    universe._modules.Add(module);
                }
            }

            return universe;
        }

        /// <summary>
        /// Starts the universe. It's not valid to run until this has been called.
        /// </summary>
        public void Start()
        {
            foreach (var module in _modules)
            {
                module.Setup();
            }
        }

        /// <summary>
        /// Advances the simulation by a number of ticks.
        /// 
        /// Ticks advance time for whatever is local to the simulation.
        /// </summary>
        /// <param name="count">The count.</param>
        public void AdvanceTicks(int count = 1)
        {
            foreach (var i in Enumerable.Range(0, count))
            {
                CurrentTime = _simulationTime.AdvanceTick();
                this._universeLifeCycle.UpdateTick();
            }
        }
    }
}
