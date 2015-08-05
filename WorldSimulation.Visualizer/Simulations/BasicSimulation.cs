using System;
using WorldSimulation.Caches;
using WorldSimulation.Caches.People;
using WorldSimulation.Caches.World;
using WorldSimulation.People;
using WorldSimulation.People.Generators;
using WorldSimulation.People.LifeEvents;
using WorldSimulation.People.LifeEvents.LifeCycle;
using WorldSimulation.People.LifeEvents.Locations;
using WorldSimulation.People.LifeEvents.Professions;
using WorldSimulation.People.LifeEvents.Relations;
using WorldSimulation.Worlds.Generators;

namespace WorldSimulation.Visualizer.Simulations
{
    public class BasicSimulation : ISimulation
    {
        public IPersonCache PersonCache { get { return _personCache; } }

        private readonly Random _random;

        // Caches
        private IPersonCache _personCache;
        private IProfessionCache _professionCache;

        // Generators
        private OrganizationGenerator _organizationGenerator;
        private GalaxyGenerator _galaxyGenerator;
        private FirstNameGenerator _firstNameGenerator;
        private LastNameGenerator _lastNameGenerator;

        // Simulation Aspects
        private Timeline _timeline;
        private Population _population;

        public BasicSimulation(Random random = null)
        {
            _random = random ?? new Random();
        }

        public void SetupSimulation(SimulationParameters simulationParameters)
        {
            _personCache = new DictionaryPersonCache();
            _professionCache = new DictionaryProfessionCache();
            _organizationGenerator = new OrganizationGenerator(new ProfessionGenerator(_random), _professionCache);
            _galaxyGenerator = new GalaxyGenerator(_personCache, _professionCache, _random);
            _firstNameGenerator = new FirstNameGenerator();
            _lastNameGenerator = new LastNameGenerator();
            _timeline = new Timeline(DateTime.Now);

            var rootTerritory = _galaxyGenerator.GenerateGalaxy(1);
            _organizationGenerator.GenerateOrganizations(rootTerritory, _random);

            _population = new Population(
                _timeline,
                new ILifeEvent[] {
			new GetMarriedLifeEvent(),
			new HaveChildrenLifeEvent(_random, _timeline),
			new HaveDivorceLifeEvent(),
			new NameChangeLifeEvent(_random,
				_firstNameGenerator,
				_lastNameGenerator),
			new DeathLifeEvent(_random, _timeline),
			new SexReassignmentLifeEvent(),
			new GenderChangeLifeEvent(_firstNameGenerator),
				new MoveLifeEvent(rootTerritory),
				new SettleLifeEvent(_random, rootTerritory),
				new GetJobLifeEvent(),
				new FiredLifeEvent()
		},
                new PersonBuilder(
                    _timeline,
                    _firstNameGenerator,
                    _lastNameGenerator,
                    _random),
                    rootTerritory,
                    _personCache,
                    _random);
        }

        public void Simulate(int years)
        {
            _timeline.AddYears(years);
        }
    }
}
