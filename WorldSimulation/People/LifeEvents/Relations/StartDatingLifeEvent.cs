using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class StartDatingLifeEvent : ILifeEvent
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
        /// <exception cref="System.NotImplementedException"></exception>
        public bool CanEncounter(Person person)
        {
            // For purposes of this simulation, we assume if you're already in a relationship,
            // you won't be dating anyone else.
            //
            // We also assume that boys and girls are gross until about 14.
            //
            // (And that there's someone to date.)
            return person.Partner == null
                   && person.Age >= 14
                   && person.Location.GetPeopleWhere(p => ValidPartner(person, p)).Any();
        }

        public IDictionary<FacetType, float> ScoreEncounter(Person enactor)
        {
            throw new NotImplementedException();
        }

        public bool Encounter(Person person)
        {
            var potentialPartner =
                person.Location.GetPeopleWhere(p => ValidPartner(person, p))
                    .OrderBy(_ => Guid.NewGuid())
                    .FirstOrDefault();
            if (potentialPartner == null)
            {
                return false;
            }

            person.Partner = potentialPartner;
            person.Log("I've started dating {0}!", potentialPartner.Name);
            potentialPartner.Log("I've started dating {0}!", person.Name);
            person.AddFlag("Dating");
            potentialPartner.AddFlag("Dating");

            return true;
        }

        private bool ValidPartner(Person seeker, Person potentialPartner)
        {
            return potentialPartner.Partner == null && potentialPartner.Age >= 14 && !potentialPartner.Equals(seeker);
        }
    }
}
