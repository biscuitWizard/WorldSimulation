using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Education
{
    public class SecondarySchoolLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
