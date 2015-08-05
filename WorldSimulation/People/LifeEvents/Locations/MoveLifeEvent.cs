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
                   && !person.HasFlag("Settled")
                   && person.Profession == null;
        }

        public bool Encounter(Person person)
        {
            var newTerritory = _availableTerritory.GetLiveableTerritories().First(t => t != person.Location);
            newTerritory.MovePerson(person);
            person.Log("Has moved to a new home at {0}", newTerritory.Name);

            if (person.Partner != null)
            {
                person.Log("I broke up with {0} to move.", person.Partner.Name);
                person.Partner.ClearFlags(FlagCategory.Romantic);
                person.ClearFlags(FlagCategory.Romantic);
                person.Partner.Partner = null;
                person.Partner = null;
            }

            return true;
        }


        public float ScoreEncounter(Person enactor)
        {
            if (enactor.HasFlag("Dating"))
            {
                return FacetInfluenceEnum.Minor.ToScore()*-1;
            }
            
            if (enactor.HasFlag("Engaged") || enactor.HasFlag("Married"))
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
