using System;
using System.Collections.Generic;

using StructureMap;
using StructureMap.Pipeline;

using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.People;
using WorldSimulation.People.Generators;
using WorldSimulation.People.LifeCycles;
using WorldSimulation.Worlds;
using WorldSimulation.Worlds.Generators;
using WorldSimulation.Worlds.LifeCycles;

namespace WorldSimulation
{
    public class UniverseFactory
    {
        private Random _random;
        private readonly IList<ILifeEvent> _lifeEvents = new List<ILifeEvent>();
        private readonly IList<Type> _lifeEventTypes = new List<Type>();
        private Territory _rootTerritory;
        private PopulationModule populationModule;
        private PersonBuilder _personBuilder;
        private readonly IContainer _container;
        private IPersonCache _personCache;
        private IProfessionCache _professionCache;
        private ILifeCycle _universeLifeCycle;
        private readonly IList<ILifeCycle> _constituentLifeCycles = new List<ILifeCycle>();
        private readonly IList<IModule> _modules = new List<IModule>();
        private bool _includePopulationLifeCycle;
        private ISimulationTime _simulationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniverseFactory"/> class.
        /// </summary>
        public UniverseFactory()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniverseFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UniverseFactory(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Sets the random generator to use for the universe.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <returns></returns>
        public UniverseFactory SetRandom(Random random)
        {
            _random = random;
            return this;
        }

        public UniverseFactory SetPersonBuilder(PersonBuilder personBuilder)
        {
            _personBuilder = personBuilder;
            return this;
        }

        /// <summary>
        /// Adds a life event to the population life cycle.
        /// </summary>
        /// <param name="lifeEvent">The life event.</param>
        /// <returns></returns>
        public UniverseFactory AddLifeEvent(ILifeEvent lifeEvent)
        {
            _includePopulationLifeCycle = true;

            _lifeEvents.Add(lifeEvent);
            return this;
        }

        /// <summary>
        /// Sets the simulation time.
        /// </summary>
        /// <param name="simulationTime">The simulation time.</param>
        /// <returns></returns>
        public UniverseFactory SetSimulationTime(ISimulationTime simulationTime)
        {
            _simulationTime = simulationTime;
            return this;
        }

        /// <summary>
        /// Adds many life events to the population life cycle.
        /// </summary>
        /// <param name="lifeEvents">The life events.</param>
        /// <returns></returns>
        public UniverseFactory AddLifeEvents(IEnumerable<ILifeEvent> lifeEvents)
        {
            _includePopulationLifeCycle = true;

            foreach (var lifeEvent in lifeEvents)
            {
                _lifeEvents.Add(lifeEvent);
            }

            return this;
        }

        public UniverseFactory AddLifeEvent<T>() where T : ILifeEvent
        {
            _includePopulationLifeCycle = true;

            _lifeEventTypes.Add(typeof(T));
            return this;
        }

        /// <summary>
        /// Sets the universe life cycle.
        /// </summary>
        /// <param name="lifeCycle">The life cycle.</param>
        /// <returns></returns>
        public UniverseFactory SetUniverseLifeCycle(ILifeCycle lifeCycle)
        {
            _universeLifeCycle = lifeCycle;
            return this;
        }

        /// <summary>
        /// Sets the root territory to be used for the universe.
        /// </summary>
        /// <param name="territory">The territory.</param>
        /// <returns></returns>
        public UniverseFactory SetRootTerritory(Territory territory)
        {
            _rootTerritory = territory;
            return this;
        }

        /// <summary>
        /// Generates a root territory using the default galaxy generator.
        /// </summary>
        /// <returns></returns>
        public UniverseFactory GenerateRootTerritory()
        {
            var galaxyGenerator = new GalaxyGenerator(_personCache, _professionCache, _random);
            _rootTerritory = galaxyGenerator.GenerateGalaxy();

            return this;
        }

        public Territory GetRootTerritory()
        {
            return _rootTerritory;
        }

        /// <summary>
        /// Includes the population life cycle. This is called if you add any life events
        /// to the universe, so no need to call it twice.
        /// </summary>
        /// <returns></returns>
        public UniverseFactory IncludePopulationLifeCycle()
        {
            _includePopulationLifeCycle = true;
            return this;
        }

        public UniverseFactory SetPersonCache(IPersonCache personCache)
        {
            _personCache = personCache;
            return this;
        }

        /// <summary>
        /// Builds a universe.
        /// </summary>
        /// <returns></returns>
        public Universe Build()
        {
            _random = _random ?? new Random();
            _universeLifeCycle = _universeLifeCycle ?? new UniverseLifeCycle();

            if (_container != null)
            {
                _personCache = _personCache ?? _container.GetInstance<IPersonCache>();
                _professionCache = _professionCache ?? _container.GetInstance<IProfessionCache>();
                _personBuilder = _personBuilder
                                 ?? new PersonBuilder(new FirstNameGenerator(), new LastNameGenerator(), _random);
                _simulationTime = _simulationTime ?? _container.GetInstance<ISimulationTime>();
                //foreach (var module in _container.With(_rootTerritory).With(_personBuilder).With(_personCache).GetAllInstances<IModule>())
                //{
                //    _modules.Add(module);
                //}
                _modules.Add(new PopulationModule(_personBuilder, _rootTerritory, _personCache));
            }

            foreach (var lifeCycle in _constituentLifeCycles)
            {
                _universeLifeCycle.AddLifeCycle(lifeCycle);
            }

            if (_includePopulationLifeCycle)
            {
                var populationLifeCycle = new PopulationLifeCycle(_personCache, _lifeEvents);

                foreach (var lifeEventType in _lifeEventTypes)
                {
                    populationLifeCycle.AddLifeEvent(_container.With(_rootTerritory).GetInstance(lifeEventType) as ILifeEvent);
                }

                _universeLifeCycle.AddLifeCycle(populationLifeCycle);
            }

            var universe = Universe.Create(_universeLifeCycle, _simulationTime ?? new MonthlySimulationTime(), _modules, _random);

            return universe;
        }
    }
}
