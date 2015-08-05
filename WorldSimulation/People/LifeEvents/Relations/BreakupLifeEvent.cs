using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;

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
                   && person.HasFlag("Dating");
        }

        float ILifeEvent.ScoreEncounter(Person enactor)
        {
            throw new NotImplementedException();
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            throw new NotImplementedException();
        }

        public bool Encounter(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
