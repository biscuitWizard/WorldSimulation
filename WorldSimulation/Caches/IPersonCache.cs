using WorldSimulation.Entities;
using WorldSimulation.People;

namespace WorldSimulation.Caches
{
    public interface IPersonCache : IDataEntityCache<Person>
    {
        void MoveToGrave(Person person);
    }
}
