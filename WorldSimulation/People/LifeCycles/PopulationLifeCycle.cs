using System;
using System.Collections.Generic;
using System.Linq;

using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.Worlds;

namespace WorldSimulation.People.LifeCycles
{
    public class PopulationLifeCycle : LifeCycleBase
    {
        private readonly IPersonCache _personCache;
        private readonly IList<ILifeEvent> _lifeEvents;

        public PopulationLifeCycle(IPersonCache personCache, IList<ILifeEvent> lifeEvents)
        {
            _personCache = personCache;
            _lifeEvents = lifeEvents;
        }

        public override void UpdateTick()
        {
            base.UpdateTick();

            //Task.WaitAll(
            //    _personCache.ReadWhere(p => !p.Deceased)
            //    .Select(person => Task.Run(() => DoLifeCycle(person)))
            //    .ToArray());
            foreach (var person in _personCache.ReadWhere(p => !p.Deceased).Select(p => p.Id.Value))
            {
                DoLifeCycle(person);
            }
        }

        public void AddLifeEvent(ILifeEvent lifeEvent)
        {
            _lifeEvents.Add(lifeEvent);
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
            if (person.IsMajorEventDate((Universe.CurrentUniverse.CurrentTime - person.BirthDate).Days / 30))
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
                                    modifier.Item2 * (Math.Abs(person.Personality.GetFacet(modifier.Item1).Value) / 10));
                    var roll = Universe.CurrentUniverse.RandomGenerator.Next(0, 100);

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

            if (person.Fate._valleys.Max() == (Universe.CurrentUniverse.CurrentTime - person.BirthDate).Days / 30)
            {
                // ded on last valley
                person.DeathDate = Universe.CurrentUniverse.CurrentTime;
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
