using System;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class HaveDivorceLifeEvent : ILifeEvent
    {
        public bool IsAvailable(Person person)
        {
            return person.Partner != null;
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Rare;
        }

        public bool Try(Person person)
        {
            person.Log("Broke up with {0}", person.Partner.Name);
            person.Partner.Log("Broke up with {0}", person.Name);
            person.Partner.History.Divorces.Add(person);
            person.Partner.Partner = null;
            person.History.Divorces.Add(person.Partner);
            person.Partner = null;

            return true;
        }
    }
}