using System;
using WorldSimulation.Entities;

namespace WorldSimulation.People.Generators
{
    public class PersonBuilder : IPersonBuilder
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

        public Person Build(Person person)
        {
            return Build(null, null);
        }

        public Person Build(Person father = null, Person mother = null)
        {
            var person = new Person(_random.NextDouble())
            {
                Age = 0,
                Sex = _random.NextDouble() > 0.5
                    ? "Female"
                    : "Male"
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

            return person;
        }
    }
}