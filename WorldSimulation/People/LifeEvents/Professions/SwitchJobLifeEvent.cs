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
        public ChancesEnum Chance
        {
            get { throw new NotImplementedException(); }
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Uncommon;
        }

        public bool IsAvailable(Person person)
        {
            return false;
        }

        public bool Try(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
