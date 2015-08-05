using System;
using System.Linq;
using WorldSimulation.Entities;
using WorldSimulation.Flags;
using WorldSimulation.Worlds;

namespace WorldSimulation.People.LifeEvents.Locations
{
    public class MoveLifeEvent : ILifeEvent
    {
        private readonly Territory _availableTerritory;

        public MoveLifeEvent(Territory availableTerritory)
        {
            _availableTerritory = availableTerritory;
        }

        public bool CanEncounter(Person person)
        {
            return person.Age > 16
                   && !person.HasFlag(TravelFlags.SettledFlag)
                   && person.Profession == null;
        }

        public bool Encounter(Person person)
        {
            var newTerritory = _availableTerritory.GetLiveableTerritories().First(t => t != person.Location);
            newTerritory.MovePerson(person);
            person.Log("I've moved to a new home at {0}", newTerritory.Name);

            if (person.Partner != null)
            {
                var mate = person.Partner;

                person.Log("I broke up with {0} to move.", mate.Name);
                mate.ClearFlags(FlagCategory.Romantic);
                person.ClearFlags(FlagCategory.Romantic);
                mate.Partner = null;
                person.Partner = null;
                person.PopulationModule.SaveChanges(mate);
            }

            return true;
        }


        public float ScoreEncounter(Person enactor)
        {
            if (enactor.HasFlag(RomanticFlags.DatingFlag))
            {
                return FacetInfluenceEnum.Minor.ToScore()*-1;
            }
            
            if (enactor.HasFlag(RomanticFlags.EngagedFlag) || enactor.HasFlag(RomanticFlags.MarriedFlag))
            {
                return FacetInfluenceEnum.Major.ToScore()*-1;
            }

            return 0;
        }

        public System.Collections.Generic.IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new[]
            {
                Tuple.Create(FacetTypeEnum.InventiveOrCurious, FacetInfluenceEnum.Minor.ToScore())
            };
        }
    }
}
