using System;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class DeathLifeEvent : ILifeEvent
    {
        private readonly Random _random;
        private readonly Timeline _timeline;

        public DeathLifeEvent(Random random,
            Timeline timeline)
        {
            _random = random;
            _timeline = timeline;
        }

        public bool CanEncounter(Person person)
        {
            return false;
        }

        public ChancesEnum CalculateChance(Person person)
        {
            if (person.Age > 100)
            {
                return ChancesEnum.Common;
            }
            else if (person.Age > 80)
            {
                return ChancesEnum.Uncommon;
            }
            else if (person.Age > 60)
            {
                return ChancesEnum.Rare;
            }
            else if (person.Age > 30)
            {
                return ChancesEnum.VeryRare;
            }

            return ChancesEnum.Impossible;
        }

        public bool Encounter(Person person)
        {
            person.DeathDate = _timeline.CurrentDate;

            return true;
        }
    }
}