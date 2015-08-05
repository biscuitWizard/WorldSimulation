using System;

using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.Generators
{
    public class PersonBuilder
    {
        private readonly FirstNameGenerator _firstNameGenerator;
        private readonly LastNameGenerator _lastNameGenerator;
        private readonly Random _random;

        public PersonBuilder(FirstNameGenerator firstNameGenerator,
            LastNameGenerator lastNameGenerator,
            Random random)
        {
            _firstNameGenerator = firstNameGenerator;
            _lastNameGenerator = lastNameGenerator;
            _random = random;
        }

        public Person Build(Person father = null, Person mother = null)
        {
            var person = new Person
            {
                Sex = _random.NextDouble() > 0.5
                    ? "Female"
                    : "Male",
                Fate = new Fate(_random.Next())
            };

            person.Gender = person.Sex;
            person.BirthDate = Universe.CurrentUniverse.CurrentTime;
            person = _firstNameGenerator.Build(person);

            if (father != null
                && mother != null)
            {
                person.FamilyName = mother.FamilyName;
            }
            else
            {
                person = _lastNameGenerator.Build(person);
            }

            // Transgender?
            if (0.063474382 > _random.NextDouble())
                person.AddFlag(IdentityFlags.TransgenderFlag);

            // Generate Personality
            person.Personality = Personality.CreateRandom(_random);

            // Generate Fate
            person.Fate.CalculateLifeline();

            return person;
        }
    }
}