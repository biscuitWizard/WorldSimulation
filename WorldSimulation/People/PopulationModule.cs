using System;
using System.Linq;

using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;
using WorldSimulation.Worlds;

namespace WorldSimulation.People
{
    public class PopulationModule : IModule
    {
        private readonly PersonBuilder _personBuilder;
        private readonly Territory _rootTerritory;
        private readonly IPersonCache _personCache;

        public PopulationModule(PersonBuilder personBuilder,
            Territory rootTerritory,
            IPersonCache personCache)
        {
            _personBuilder = personBuilder;
            _rootTerritory = rootTerritory;
            _personCache = personCache;
        }

        public Person CreatePerson(Person father, Person mother)
        {
            var child = _personBuilder.Build(father, mother);
            child.PopulationModule = this;
            child.Location = mother.Location;

            child = _personCache.Save(child);
            return child;
        }

        public void SaveChanges(Person person)
        {
            _personCache.Save(person);
        }

        public void Setup()
        {
            var territory = _rootTerritory.GetLiveableTerritories();

            for (var i = 0; i < 200; i++)
            {
                var person = _personBuilder.Build();
                person.PopulationModule = this;
                person.Location = territory.OrderBy(_ => Guid.NewGuid()).First();
                _personCache.Save(person);
            }
        }
    }
}