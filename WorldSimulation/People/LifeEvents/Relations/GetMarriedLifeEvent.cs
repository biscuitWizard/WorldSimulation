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
            var mate = person.Partner;

            // Mate them up!
            person.Log("I married {0}.", mate.Name);
            mate.Log("I married {0}. ", person.Name);
            person.AddFlag(RomanticFlags.MarriedFlag);
            mate.AddFlag(RomanticFlags.MarriedFlag);

            if (person.HasFlag(RomanticFlags.EngagedFlag))
            {
                person.ClearFlag(RomanticFlags.EngagedFlag);
                mate.ClearFlag(RomanticFlags.EngagedFlag);
            }

            person.Population.SaveChanges(mate);

            return true;
        }
    }
}