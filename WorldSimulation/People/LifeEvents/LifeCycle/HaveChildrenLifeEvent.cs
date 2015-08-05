using System;
using System.Collections.Generic;
using System.Linq;

using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class HaveChildrenLifeEvent : ILifeEvent
    {
        private const float Chance = 0.66167569289f;

        /// <summary>
        /// Simple gate-keeping check to determine if it is even physically possible for this event
        /// to be triggered.
        /// An example would be lighting a car on fire without a car. Without the car,
        /// you couldn't light the fire on said car.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>
        /// Whether or not this encounter can be encountered.
        /// </returns>
        public bool CanEncounter(Person person)
        {
            return person.Age >= 16
                   && person.Sex == "Female";
        }

        /// <summary>
        /// Scoring an encounter takes various bits about a person and calculates a score
        /// that will be used to determine whether or not this person will do this event.
        /// (This is like a Target Number Modifier in more colloquial games like Dungeons and Dragons,
        /// or Shadowrun)
        /// This specific score provides situational-based modifiers.
        /// </summary>
        /// <param name="enactor">The enactor.</param>
        /// <returns></returns>
        public float ScoreEncounter(Person enactor)
        {
            float score = 0;
            if (enactor.Partner != null)
            {
                score += FacetInfluenceEnum.Minor.ToScore();
            }

            if (enactor.HasFlag(RomanticFlags.DatingFlag))
            {
                score += FacetInfluenceEnum.Minor.ToScore();
            }
            else if (enactor.HasFlag(RomanticFlags.MarriedFlag))
            {
                score += FacetInfluenceEnum.Moderate.ToScore();
            }

            return score;
        }

        /// <summary>
        /// Scoring an encounter takes various bits about a person and calculates a score
        /// that will be used to determine whether or not this person will do this event.
        /// This specific score takes in Facets and modifiers the base score
        /// by the amount of that person's personality and the int modifier.
        /// </summary>
        /// <returns></returns>
        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new []
            {
                Tuple.Create(FacetTypeEnum.FriendlyOrCompassionate, FacetInfluenceEnum.Minor.ToScore())
            };
        }

        public bool Encounter(Person person)
        {
            if ((Chance/(person.Children.Count*2)) < Universe.CurrentUniverse.RandomGenerator.NextDouble())
                return false;

            var mate = person.Partner;

            if (mate == null)
            {
                // We're having sex with a stranger!
                var stranger =
                    person.Location.GetPeopleWhere(p => p.Partner == null && !p.Equals(person)).FirstOrDefault();

                if (stranger == null)
                {
                    // No one to have sex with.
                    return false;
                }

                mate = stranger;
            }

            var child = person.PopulationModule.CreatePerson(person, mate);
            person.Children.Add(child);
            mate.Children.Add(child);
            child.Parents = new Person[2];
            child.Parents[0] = person;
            child.Parents[1] = mate;
            person.Log("I had a child with {0}.", mate.FirstName);
            mate.Log("I had a child with {0}", person.FirstName);

            if (Universe.CurrentUniverse.RandomGenerator.NextDouble() < 0.12)
            {
                person.Log("Passed away having a child.");
                person.DeathDate = Universe.CurrentUniverse.CurrentTime;
            }

            return true;
        }
    }
}