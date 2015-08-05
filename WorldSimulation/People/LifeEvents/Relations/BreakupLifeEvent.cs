using System;
using System.Collections.Generic;
using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class BreakupLifeEvent : ILifeEvent
    {
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
            return person.Partner != null
                && person.HasFlag(RomanticFlags.DatingFlag)
                && person.Partner.HasFlag(RomanticFlags.DatingFlag);
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
        float ILifeEvent.ScoreEncounter(Person enactor)
        {
            return FacetInfluenceEnum.VeryMinor.ToScore() * -1;
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
            return new Tuple<FacetTypeEnum, int>[0];
        }

        public bool Encounter(Person person)
        {
            person.Log("I broke up with {0}.", person.Partner.Name);
            person.Partner.Log("I broke up with {0}.", person.Name);

            var mate = person.Partner;
            if (person.HasFlag(RomanticFlags.DatingFlag))
            {
                mate.ClearFlag(RomanticFlags.DatingFlag);
                person.ClearFlag(RomanticFlags.DatingFlag);
            }
            if (person.HasFlag(RomanticFlags.EngagedFlag))
            {
                mate.ClearFlag(RomanticFlags.EngagedFlag);
                person.ClearFlag(RomanticFlags.EngagedFlag);
            }

            mate.Partner = null;
            person.Partner = null;

            person.Population.SaveChanges(mate);

            return true;
        }
    }
}
