using System;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents
{
    public class SexReassignmentLifeEvent : ILifeEvent
    {
        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Uncommon;
        }

        public bool CanEncounter(Person person)
        {
            return person.HasFlag("Transgender") && person.HasFlag("Transitioning");
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