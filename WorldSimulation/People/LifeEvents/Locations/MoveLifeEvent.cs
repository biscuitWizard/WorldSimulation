using System.Linq;
using WorldSimulation.Entities;
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

        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.Rare;
        }

        public bool Encounter(Person person)
        {
            var newTerritory = _availableTerritory.GetLiveableTerritories().First(t => t != person.Location);
            newTerritory.MovePerson(person);
            person.Log("Has moved to a new home at {0}", newTerritory.Name);

            return true;
        }
    }
}
