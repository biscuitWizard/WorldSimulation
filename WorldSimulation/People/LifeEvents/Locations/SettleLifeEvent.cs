﻿using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Entities;
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
                || (person.HasFlag("Orphan") && person.Age > 12))
                && !person.HasFlag("Settled")
                && person.Profession == null;
        }

        public float ScoreEncounter(Person enactor)
        {
            throw new NotImplementedException();
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            throw new NotImplementedException();
        }

        public bool Encounter(Person person)
        {
            var finalTerritory = person.Location;
            if (_random.SuccessfulChance(ChanceToMove))
            {
                finalTerritory = _rootTerritory.GetLiveableTerritories().First(t => t != finalTerritory);
            }

            person.AddFlag("Settled");
            finalTerritory.MovePerson(person);
            person.Log("I have settled permanently at {0}", person.Location.Name);

            return true;
        }
    }
}
