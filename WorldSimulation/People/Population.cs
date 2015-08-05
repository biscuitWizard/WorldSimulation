using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;
using WorldSimulation.Worlds;

namespace WorldSimulation.People
{
    public class Population
    {
        private const int MaxLifeEventQuota = 120;

        private readonly IList<ILifeEvent> _lifeEvents;
        private readonly PersonBuilder _personBuilder;
        private readonly IPersonCache _personCache;
        private readonly Random _random;
        private readonly Timeline _timeline;
        private readonly Territory _rootTerritory;

        private readonly IDictionary<ulong, Tuple<int, int>> _playerLifeEventQuotas
            = new Dictionary<ulong, Tuple<int, int>>();

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
            var territory = rootTerritory.GetLiveableTerritories().First();

            for (var i = 0; i < 24; i++)
            {
                var person = _personBuilder.Build(null, null);
                person.Population = this;
                person.Location = territory;
                person = personCache.Save(person);
                _playerLifeEventQuotas.Add(person.Id.Value, Tuple.Create(0, MaxLifeEventQuota));
            }
            timeline.MonthElapsed += Timeline_MonthElapsed;
        }

        public Person CreatePerson(Person father, Person mother)
        {
            var child = _personBuilder.Build(father, mother);
            child.Population = this;
            child.Location = mother.Location;

            child = _personCache.Save(child);
            _playerLifeEventQuotas.Add(child.Id.Value, Tuple.Create(0, MaxLifeEventQuota));
            return child;
        }

        protected void Timeline_MonthElapsed(object timeline, EventArgs args)
        {
            //Task.WaitAll(
            //    _personCache.ReadWhere(p => !p.Deceased)
            //    .Select(person => Task.Run(() => DoLifeCycle(person)))
            //    .ToArray());
            foreach (var person in _personCache.ReadWhere(p => !p.Deceased))
            {
                DoLifeCycle(person);
            }
        }

        protected void DoLifeCycle(Person person)
        {
            if (person.BirthDate.Month == _timeline.Month)
            {
                // Happy birthday!
                person.Age++;
                _playerLifeEventQuotas[person.Id.Value] = Tuple.Create(0, MaxLifeEventQuota);
            }

            //var usedQuota = _playerLifeEventQuotas[person.Id.Value].Item1;

            //// Life events for everyone!
            //var lifeEvents =
            //    _lifeEvents.Where(le => (int)le.Size + usedQuota < MaxLifeEventQuota)
            //    .Where(le => le.CanEncounter(person))
            //    .Where(le => _random.SuccessfulChance(le.CalculateChance(person)))
            //    .ToList();
            //foreach (var lifeEvent in lifeEvents)
            //{
            //    // Just in case this has changed.
            //    if (usedQuota + (int) lifeEvent.Size > MaxLifeEventQuota)
            //    {
            //        continue;
            //    }

            //    var success = lifeEvent.Encounter(person);
            //    if (!success) continue;

            //    usedQuota += (int)lifeEvent.Size;
            //    _playerLifeEventQuotas[person.Id.Value] = Tuple.Create(usedQuota, MaxLifeEventQuota);

            //    // Oh no, they're newly deceased.
            //    if (person.Deceased)
            //    {
            //        _personCache.MoveToGrave(person);
            //        _playerLifeEventQuotas.Remove(person.Id.Value);
            //        return;
            //    }
            //}

            if (person.IsMajorEventDate((_timeline.CurrentDate - person.BirthDate).Days/30))
            {
                var lifeEvent =
                    _lifeEvents.Where(le => le.CanEncounter(person)).OrderBy(le => Guid.NewGuid()).FirstOrDefault();

                if (lifeEvent != null)
                {
                    lifeEvent.Encounter(person);
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
                _playerLifeEventQuotas.Remove(person.Id.Value);
                return;
            }
        }
    }
}