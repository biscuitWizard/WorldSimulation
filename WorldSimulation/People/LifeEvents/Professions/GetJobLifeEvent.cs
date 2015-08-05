using System;
using System.Collections.Generic;
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

        public float ScoreEncounter(Person enactor)
        {
            return 0;
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new []
            {
                Tuple.Create(FacetTypeEnum.SecureOrConfident, FacetInfluenceEnum.Moderate.ToScore())
            };
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
    }
}
