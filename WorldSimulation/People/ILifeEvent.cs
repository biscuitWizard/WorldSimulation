using System.Collections.Generic;
using WorldSimulation.Entities;

namespace WorldSimulation.People
{
    public interface ILifeEvent
    {
        /// <summary>
        /// Simple gate-keeping check to determine if it is even physically possible for this event
        /// to be triggered.
        /// 
        /// An example would be lighting a car on fire without a car. Without the car,
        /// you couldn't light the fire on said car.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>Whether or not this encounter can be encountered.</returns>
        bool CanEncounter(Person person);

        /// <summary>
        /// Scoring an encounter takes various bits about a person and calculates a score
        /// that will be used to determine whether or not this person will do this event.
        /// 
        /// (This is like a Target Number Modifier in more colloquial games like Dungeons and Dragons,
        /// or Shadowrun)
        /// </summary>
        /// <param name="enactor">The enactor.</param>
        /// <returns></returns>
        IDictionary<FacetType, float> ScoreEncounter(Person enactor); 

        bool Encounter(Person person);
    }
}