using System;
using System.Collections.Generic;

using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class SexReassignmentLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.HasFlag(IdentityFlags.TransgenderFlag) 
                && person.HasFlag(IdentityFlags.TransitioningFlag);
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
            person.AddFlag(IdentityFlags.TransitionedFlag);
            person.ClearFlag(IdentityFlags.TransitioningFlag);
            person.Sex = person.Gender;
            person.Log("I had a sex change to {0}.", person.Sex);

            return true;
        }
    }
}