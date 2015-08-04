using WorldSimulation.Entities;

namespace WorldSimulation.People
{
    public interface IPersonBuilder
    {
        Person Build(Person person);
    }
}