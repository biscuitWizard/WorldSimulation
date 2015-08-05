using System;
using WorldSimulation.Entities;
using WorldSimulation.People.Generators;

namespace WorldSimulation.People.LifeEvents
{
    public class NameChangeLifeEvent : ILifeEvent
    {
        private readonly FirstNameGenerator _firstNameGenerator;
        private readonly LastNameGenerator _lastNameGenerator;
        private readonly Random _random;

        public NameChangeLifeEvent(Random random, FirstNameGenerator firstNameGenerator,
            LastNameGenerator lastNameGenerator)
        {
            _random = random;
            _firstNameGenerator = firstNameGenerator;
            _lastNameGenerator = lastNameGenerator;
        }

        public bool CanEncounter(Person person)
        {
            return person.Age >= 18;
        }

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.UnbelievablyRare;
        }

        public bool Encounter(Person person)
        {
            if (.5 < _random.NextDouble())
                _firstNameGenerator.Build(person);
            else
                _lastNameGenerator.Build(person);
            person.Log("Changed name to {0} {1}.", person.FirstName, person.FamilyName);

            return true;
        }
    }
}