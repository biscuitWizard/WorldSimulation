using System;
using System.Linq;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class GetMarriedLifeEvent : ILifeEvent
    {
        public bool IsAvailable(Person person)
        {
            return person.Age >= 16
                   && person.Partner == null;
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Uncommon;
        }

        public bool Try(Person person)
        {
            var mate = FindMate(person.Population, person);
            if (mate == null)
                return false;

            // Mate them up!
            person.Partner = mate;
            mate.Partner = person;
            person.Log("Hooked up with " + mate.FirstName);
            mate.Log("Hooked up with " + person.FirstName);

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