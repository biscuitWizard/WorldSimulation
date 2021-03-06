﻿using System;
using System.Collections.Generic;

using WorldSimulation.Entities;
using WorldSimulation.Flags;
using WorldSimulation.People.Generators;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class GenderChangeLifeEvent : ILifeEvent
    {
        private readonly FirstNameGenerator _firstNameGenerator;

        public GenderChangeLifeEvent(FirstNameGenerator firstNameGenerator)
        {
            this._firstNameGenerator = firstNameGenerator;
        }

        public bool CanEncounter(Person person)
        {
            return person.Age >= 8
                   && person.HasFlag(IdentityFlags.TransgenderFlag)
                   && !person.HasFlag(IdentityFlags.TransitionedFlag)
                   && !person.HasFlag(IdentityFlags.TransitioningFlag);
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
            person.FirstName = this._firstNameGenerator.Build(person).FirstName;
            person.Log(string.Format("Changed their name to {0} {1} and is now transistioning", person.FirstName,
                person.FamilyName));
            person.AddFlag(IdentityFlags.TransitioningFlag);

            return true;
        }
    }
}