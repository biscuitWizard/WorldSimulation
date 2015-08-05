using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class GetEngagedLifeEvent : ILifeEvent
    {
        private readonly Random _random;

        public GetEngagedLifeEvent(Random random)
        {
            _random = random;
        }

        public bool CanEncounter(Person person)
        {
            return person.Partner != null
                   && person.Partner.HasFlag(RomanticFlags.DatingFlag)
                   && person.HasFlag(RomanticFlags.DatingFlag);
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
            var month = _random.Next(3, 8);
            person.AddFlag(RomanticFlags.EngagedFlag);
            person.Partner.AddFlag(RomanticFlags.EngagedFlag);
            person.RemoveFlag(RomanticFlags.DatingFlag);
            person.Partner.RemoveFlag(RomanticFlags.DatingFlag);

            person.Population.SaveChanges(person.Partner);

            return true;
        }
    }
}
