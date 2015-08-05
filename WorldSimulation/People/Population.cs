using System;
using System.Collections.Generic;
using System.Linq;

using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;
using WorldSimulation.Worlds;

namespace WorldSimulation.People
{
    public class Population
    {
        private readonly IList<ILifeEvent> _lifeEvents;
        private readonly PersonBuilder _personBuilder;
        private readonly IPersonCache _personCache;
        private readonly Random _random;
        private readonly Timeline _timeline;
        private readonly Territory _rootTerritory;

        public Population(Timeline timeline,
            IList<ILifeEvent> lifeEvents,
            PersonBuilder personBuilder,
            Territory rootTerritory,
            IPersonCache personCache,
            Random random)
        {
            _rootTerritory = rootTerritory;
            _timeline = timeline;
            _lifeEvents = lifeEvents;
            _personBuilder = personBuilder;
            _personCache = personCache;
            _random = random;

            // Random location for start pop.
            var territory = rootTerritory.GetLiveableTerritories();

            for (var i = 0; i < 200; i++)
            {
                var person = _personBuilder.Build();
                person.Population = this;
                person.Location = territory.OrderBy(_ => Guid.NewGuid()).First();
                personCache.Save(person);
            }
            timeline.MonthElapsed += Timeline_MonthElapsed;
        }

        public Person CreatePerson(Person father, Person mother)
        {
            var child = _personBuilder.Build(father, mother);
            child.Population = this;
            child.Location = mother.Location;

            child = _personCache.Save(child);
            return child;
        }

        protected void Timeline_MonthElapsed(object timeline, EventArgs args)
        {
            //Task.WaitAll(
            //    _personCache.ReadWhere(p => !p.Deceased)
            //    .Select(person => Task.Run(() => DoLifeCycle(person)))
            //    .ToArray());
            foreach (var person in _personCache.ReadWhere(p => !p.Deceased).Select(p => p.Id.Value))
            {
                DoLifeCycle(person);
            }
        }

        public void SaveChanges(Person person)
        {
            _personCache.Save(person);
        }

        protected void DoLifeCycle(ulong personId)
        {
            var person = _personCache.Read(personId);

            if (person.Deceased)
            {
                // Just check for expired data.
                return;
            }

            // For the purposes of a simple simulation, only 0-1 life events can happen in a single tick.
            if (person.IsMajorEventDate((_timeline.CurrentDate - person.BirthDate).Days/30))
            {
                var availableLifeEvents = _lifeEvents.Where(lifeEvent => lifeEvent.CanEncounter(person));
                foreach (var lifeEvent in availableLifeEvents)
                {
                    var baseScore = lifeEvent.ScoreEncounter(person);
                    var scoreModifier =
                        lifeEvent.ScorePersonalityEncounter()
                            .Where(
                                modifier => person.Personality.GetFacet(modifier.Item1).DominantPole == modifier.Item1)
                            .Sum(
                                modifier =>
                                    modifier.Item2*(Math.Abs(person.Personality.GetFacet(modifier.Item1).Value)/10));
                    var roll = _random.Next(0, 100);

                    if (10 + baseScore + scoreModifier >= roll)
                    {
                        // Success!
                        var eventSuccess = lifeEvent.Encounter(person);

                        if (eventSuccess)
                        {
                            break;
                        }
                    }
                }
            }

            if (person.Fate._valleys.Max() == (_timeline.CurrentDate - person.BirthDate).Days/30)
            {
                // ded on last valley
                person.DeathDate = _timeline.CurrentDate;
            }

            // Oh no, they're newly deceased.
            if (person.Deceased)
            {
                _personCache.MoveToGrave(person);
            }
            else
            {
                _personCache.Save(person);
            }
        }
    }
}