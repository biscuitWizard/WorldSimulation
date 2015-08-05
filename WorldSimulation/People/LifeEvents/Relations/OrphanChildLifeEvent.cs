using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Relations
{
    /// <summary>
    /// LifeEvent for parent abandoning a child.
    /// </summary>
    public class OrphanChildLifeEvent : ILifeEvent
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
            return person.Children.Any(c => c.Age < 3);
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
        /// <exception cref="System.NotImplementedException"></exception>
        float ILifeEvent.ScoreEncounter(Person enactor)
        {
            return -5;
        }

        /// <summary>
        /// Scoring an encounter takes various bits about a person and calculates a score
        /// that will be used to determine whether or not this person will do this event.
        /// This specific score takes in Facets and modifiers the base score
        /// by the amount of that person's personality and the int modifier.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new Tuple<FacetTypeEnum, int>[0];
        }

        /// <summary>
        /// Encounters the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Encounter(Person person)
        {
            // We're orphaning a child! Fuck you, kid!
            var child = person.Children.OrderByDescending(c => c.Age).First();
            foreach (var parent in child.Parents)
            {
                parent.Children.Remove(child);
            }
            child.Parents = null;
            child.AddFlag("Orphan");

            return true;
        }
    }
}
