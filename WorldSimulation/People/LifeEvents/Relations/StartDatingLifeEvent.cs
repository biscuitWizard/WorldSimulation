using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldSimulation.Entities;
using WorldSimulation.Flags;

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
                   && !person.HasFlag(RomanticFlags.DatingFlag)
                   && !person.HasFlag(RomanticFlags.EngagedFlag)
                   && !person.HasFlag(RomanticFlags.MarriedFlag)
                   && person.Age >= 14
                   && person.Location.GetPeopleWhere(p => ValidPartner(person, p)).Any();
        }

        public float ScoreEncounter(Person enactor)
        {
            return FacetInfluenceEnum.Minor.ToScore();
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new Tuple<FacetTypeEnum, int>[0];
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

            person.Log("I've started dating {0}!", potentialPartner.Name);
            potentialPartner.Log("I've started dating {0}!", person.Name);
            person.AddFlag(RomanticFlags.DatingFlag);
            potentialPartner.AddFlag(RomanticFlags.DatingFlag);

            person.Partner = potentialPartner;
            person.PopulationModule.SaveChanges(potentialPartner);

            return true;
        }

        private static bool ValidPartner(Person seeker, Person potentialPartner)
        {
            return !potentialPartner.HasFlag(RomanticFlags.DatingFlag) || !potentialPartner.HasFlag(RomanticFlags.MarriedFlag) ||
                   !potentialPartner.HasFlag(RomanticFlags.EngagedFlag) && potentialPartner.Age >= 14 &&
                   potentialPartner.Id != seeker.Id;
        }
    }
}
