using System;
using System.Linq;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Professions
{
    public class GetJobLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.Profession == null
                   && person.Age >= 16;
        }

        public bool Encounter(Person person)
        {
            var professions =
                person.Location.GetAvailableProfessions().Where(p => p.DoesMeetRequirements(person)).ToList();
            if (!professions.Any())
            {
                return false;
            }

            person.Profession = professions.OrderBy(_ => Guid.NewGuid()).First();
            person.Log("I just got a new job as a {0} at {1}!", person.Profession.Title, person.Profession.Company);
            return true;
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Common;
        }
    }
}
