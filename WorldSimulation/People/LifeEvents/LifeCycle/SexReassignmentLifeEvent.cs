using System;
using System.Collections.Generic;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents
{
    public class SexReassignmentLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.HasFlag("Transgender") 
                && person.HasFlag("Transitioning");
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
            person.AddFlag("Transistioned");
            person.RemoveFlag("Transistioning");
            person.Sex = person.Gender;
            person.Log("Had a sex change to {0}.", person.Sex);

            return true;
        }
    }
}