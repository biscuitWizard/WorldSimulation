using System;
using System.Collections.Generic;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Professions
{
    public class FiredLifeEvent : ILifeEvent
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
            return person.Profession != null;
        }

        public bool Encounter(Person person)
        {
            person.Profession = null;
            person.Log("I got fired.");

            return true;
        }

        float ILifeEvent.ScoreEncounter(Person enactor)
        {
            return 0;
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
            return new[]
            {
                Tuple.Create(FacetTypeEnum.EasyGoingOrCareless, FacetInfluenceEnum.Moderate.ToScore())
            };
        }
    }
}
