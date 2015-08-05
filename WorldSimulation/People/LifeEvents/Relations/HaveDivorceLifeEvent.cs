using System;
using System.Linq;
using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class HaveDivorceLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.Partner != null
                && person.HasFlag(RomanticFlags.MarriedFlag)
                && person.Partner.HasFlag(RomanticFlags.MarriedFlag);
        }

        public bool Encounter(Person person)
        {
            var mate = person.Partner;
            person.Log("I had a divorce with {0}.", mate.Name);
            mate.Log("I had a divorce with {0}.", person.Name);
            person.RemoveFlag(RomanticFlags.MarriedFlag);
            mate.RemoveFlag(RomanticFlags.MarriedFlag);
            mate.History.Divorces.Add(person);
            person.History.Divorces.Add(mate);
            mate.Partner = null;
            person.Partner = null;

            person.Population.SaveChanges(mate);

            return true;
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
            return enactor.Personality.GetFacets()
                .Where(
                    facet =>
                        enactor.Personality.GetDominantFacetType(facet) !=
                        enactor.Partner.Personality.GetDominantFacetType(facet))
                .Sum(facet => FacetInfluenceEnum.Minor.ToScore());
        }

        /// <summary>
        /// Scoring an encounter takes various bits about a person and calculates a score
        /// that will be used to determine whether or not this person will do this event.
        /// This specific score takes in Facets and modifiers the base score
        /// by the amount of that person's personality and the int modifier.
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new[]
            {
                Tuple.Create(FacetTypeEnum.SensitiveOrNervous, FacetInfluenceEnum.VeryMinor.ToScore()),
                Tuple.Create(FacetTypeEnum.AnalyticalOrDetached, FacetInfluenceEnum.VeryMinor.ToScore())
            };
        }
    }
}