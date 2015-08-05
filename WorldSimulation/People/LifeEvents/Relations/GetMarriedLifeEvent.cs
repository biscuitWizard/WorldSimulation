using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class GetMarriedLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.Partner != null
                   && person.Partner.HasFlag(RomanticFlags.EngagedFlag)
                   && person.HasFlag(RomanticFlags.EngagedFlag);
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
            var mate = FindMate(person.Population, person);
            if (mate == null)
                return false;

            // Mate them up!
            person.Partner = mate;
            mate.Partner = person;
            person.Log("I married {0}.", mate.Name);
            mate.Log("I married {0}. ", person.Name);
            person.AddFlag(RomanticFlags.MarriedFlag);
            mate.AddFlag(RomanticFlags.MarriedFlag);

            if (person.HasFlag(RomanticFlags.EngagedFlag))
            {
                person.RemoveFlag(RomanticFlags.EngagedFlag);
                mate.RemoveFlag(RomanticFlags.EngagedFlag);
            }

            return true;
        }

        protected virtual Person FindMate(Population population, Person seeker)
        {
            var potentialMates = seeker.Location.GetPeopleWhere(p =>
                !p.Deceased && p.Age > 16 && p.Partner == null && seeker.Age - p.Age < 7 &&
                p.Age - seeker.Age < 6 && p.Id != seeker.Id);
            return !potentialMates.Any() ? null : potentialMates.First();
        }
    }
}