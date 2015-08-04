using System;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;

namespace WorldSimulation.People.LifeEvents
{
    public class GenderChangeLifeEvent : ILifeEvent
    {
        private readonly FirstNameGenerator _firstNameGenerator;

        public GenderChangeLifeEvent(FirstNameGenerator firstNameGenerator)
        {
            _firstNameGenerator = firstNameGenerator;
        }

        public bool IsAvailable(Person person)
        {
            return person.Age >= 8
                   && person.HasFlag("Transgender")
                   && !person.HasFlag("Transistioned")
                   && !person.HasFlag("Transistioning");
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Rare;
        }

        public bool Try(Person person)
        {
            person.Gender = person.Gender == "Female"
                ? "Male"
                : "Female";
            person.FirstName = _firstNameGenerator.Build(person).FirstName;
            person.Log(string.Format("Changed their name to {0} {1} and is now transistioning", person.FirstName,
                person.FamilyName));
            person.AddFlag("Transistioning");

            return true;
        }
    }
}