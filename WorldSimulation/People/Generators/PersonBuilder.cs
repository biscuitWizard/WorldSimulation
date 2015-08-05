using System;
using WorldSimulation.Entities;

namespace WorldSimulation.People.Generators
{
    public class PersonBuilder
    {
        private readonly FirstNameGenerator _firstNameGenerator;
        private readonly LastNameGenerator _lastNameGenerator;
        private readonly Random _random;
        private readonly Timeline _timeline;

        public PersonBuilder(Timeline timeline,
            FirstNameGenerator firstNameGenerator,
            LastNameGenerator lastNameGenerator,
            Random random)
        {
            _timeline = timeline;
            _firstNameGenerator = firstNameGenerator;
            _lastNameGenerator = lastNameGenerator;
            _random = random;
        }

        public Person Build(Person father = null, Person mother = null)
        {
            var person = new Person(_timeline)
            {
                Sex = _random.NextDouble() > 0.5
                    ? "Female"
                    : "Male",
                Fate = new Fate(_random.Next())
            };

            person.Gender = person.Sex;
            person.BirthDate = _timeline.CurrentDate;
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
                person.AddFlag("Transgender");

            // Generate Personality
            person.Personality = Personality.CreateRandom(_random);

            // Generate Fate
            person.Fate.CalculateLifeline();

            return person;
        }
    }
}