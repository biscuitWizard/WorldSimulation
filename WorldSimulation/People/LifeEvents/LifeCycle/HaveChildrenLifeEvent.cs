using System;
using System.Collections.Generic;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class HaveChildrenLifeEvent : ILifeEvent
    {
        private const float Chance = 0.66167569289f;
        private readonly Random _random;
        private readonly Timeline _timeline;

        public HaveChildrenLifeEvent(Random random, Timeline timeline)
        {
            _random = random;
            _timeline = timeline;
        }

        public bool CanEncounter(Person person)
        {
            return person.Age >= 18
                   && person.Partner != null
                   && person.Partner.Sex != person.Sex
                   && person.Sex == "Female";
        }

        public float ScoreEncounter(Person enactor)
        {
            throw new NotImplementedException();
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            throw new NotImplementedException();
        }

        public bool Encounter(Person person)
        {
            if ((Chance/(person.Children.Count*2)) < _random.NextDouble())
                return false;

            var child = person.Population.CreatePerson(person, person.Partner);
            person.Children.Add(child);
            person.Partner.Children.Add(child);
            child.Parents = new Person[2];
            child.Parents[0] = person;
            child.Parents[1] = person.Partner;
            person.Log("Had a child with {0}", person.Partner.FirstName);
            person.Partner.Log("Had a child with {0}", person.FirstName);

            if (_random.NextDouble() < 0.12)
            {
                person.Log("Passed away having a child.");
                person.DeathDate = _timeline.CurrentDate;
            }

            return true;
        }
    }
}