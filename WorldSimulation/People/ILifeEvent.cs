using WorldSimulation.Entities;

namespace WorldSimulation.People
{
    public interface ILifeEvent
    {
        bool IsAvailable(Person person);
        bool Try(Person person);
    }
}