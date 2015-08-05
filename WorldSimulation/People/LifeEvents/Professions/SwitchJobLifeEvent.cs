using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Professions
{
    public class SwitchJobLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return false;
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
