using System;
using System.Linq;

using StructureMap;

using WorldSimulation.Caches;
using WorldSimulation.Caches.People;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;
using WorldSimulation.People.LifeEvents;
using WorldSimulation.People.LifeEvents.LifeCycle;
using WorldSimulation.People.LifeEvents.Locations;
using WorldSimulation.People.LifeEvents.Professions;
using WorldSimulation.People.LifeEvents.Relations;
using WorldSimulation.Worlds;

namespace WorldSimulation.Visualizer.Simulations
{
    public class BasicSimulation : ISimulation
    {
        public IPersonCache PersonCache { get; private set; }
        public Territory RootTerritory { get; private set; }

        private readonly IContainer _container;
        private UniverseFactory _universeFactory;
        private Universe _universe;
        private SimulationParameters _simulationParameters;

        public BasicSimulation(IContainer container)
        {
            _container = container;    
        }

        public void SetupSimulation(SimulationParameters simulationParameters)
        {
            PersonCache = _container.GetInstance<IPersonCache>();

            _simulationParameters = simulationParameters;
            _universeFactory = new UniverseFactory(_container);

            _universeFactory.SetRandom(_container.GetInstance<Random>());
            _universeFactory.SetPersonCache(PersonCache);
            _universeFactory.SetPersonBuilder(
                new PersonBuilder(
                    _container.GetInstance<FirstNameGenerator>(),
                    _container.GetInstance<LastNameGenerator>(),
                    _container.GetInstance<Random>()));
            _universeFactory.GenerateRootTerritory();

            _universeFactory.AddLifeEvent<GetMarriedLifeEvent>()
                .AddLifeEvent<StartDatingLifeEvent>()
                .AddLifeEvent<OrphanChildLifeEvent>()
                .AddLifeEvent<GetEngagedLifeEvent>()
                .AddLifeEvent<BreakupLifeEvent>()
                .AddLifeEvent<SwitchJobLifeEvent>()
                .AddLifeEvent<HaveChildrenLifeEvent>()
                .AddLifeEvent<DeathLifeEvent>()
                .AddLifeEvent<SexReassignmentLifeEvent>()
                .AddLifeEvent<GenderChangeLifeEvent>()
                .AddLifeEvent<MoveLifeEvent>()
                .AddLifeEvent<SettleLifeEvent>()
                .AddLifeEvent<GetJobLifeEvent>()
                .AddLifeEvent<FiredLifeEvent>();

            _universe = _universeFactory.Build();

            RootTerritory = _universeFactory.GetRootTerritory();

            // TODO: Move this eventually:
            _universe.Start();
        }

        public void Simulate(int ticks)
        {
            _universe.AdvanceTicks(ticks);
        }
    }
}
