using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Relations
{
    public class OrphanedLifeEvent : ILifeEvent
    {
        public ChancesEnum CalculateChance(Person person)
        {
            return ChancesEnum.VeryRare;
        }

        public bool IsAvailable(Person person)
        {
            return person.Age < 3
                   && !person.HasFlag("Orphaned");
        }

        public bool Try(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}
