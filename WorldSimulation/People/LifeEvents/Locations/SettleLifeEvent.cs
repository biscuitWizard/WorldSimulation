using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Entities;
using WorldSimulation.Flags;
using WorldSimulation.Worlds;

namespace WorldSimulation.People.LifeEvents.Locations
{
    /// <summary>
    /// Life event that finds the location that a person will settle down at
    /// 
    /// and settles them there permanently.
    /// </summary>
    public class SettleLifeEvent : ILifeEvent
    {
        private readonly Random _random;
        private readonly Territory _rootTerritory;

        public SettleLifeEvent(Random random, Territory rootTerritory)
        {
            _random = random;
            _rootTerritory = rootTerritory;
        }

        public bool CanEncounter(Person person)
        {
            return (person.Age > 16
                || (person.HasFlag(IdentityFlags.OrphanFlag) && person.Age > 12))
                && !person.HasFlag(TravelFlags.SettledFlag)
                && person.Profession == null;
        }

        public float ScoreEncounter(Person enactor)
        {
            return 0;
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new Tuple<FacetTypeEnum, int>[0];
        }

        public bool Encounter(Person person)
        {
            var finalTerritory = _rootTerritory.GetLiveableTerritories().First(t => t != person.Location);

            person.AddFlag(TravelFlags.SettledFlag);
            finalTerritory.MovePerson(person);
            person.Log("I have settled permanently at {0}.", person.Location.Name);

            return true;
        }
    }
}
