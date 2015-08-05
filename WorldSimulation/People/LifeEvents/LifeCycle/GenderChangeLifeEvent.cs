using System;
using System.Collections.Generic;
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

        public bool CanEncounter(Person person)
        {
            return person.Age >= 8
                   && person.HasFlag("Transgender")
                   && !person.HasFlag("Transistioned")
                   && !person.HasFlag("Transistioning");
        }

        public float ScoreEncounter(Person enactor)
        {
            return 0;
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new Tuple<FacetTypeEnum, int>[0];
        }

        public bool Encounter(Person person)
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