using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Professions
{
    public class FiredLifeEvent : ILifeEvent
    {
        public bool IsAvailable(Person person)
        {
            return person.Profession != null;
        }

        public bool Try(Person person)
        {
            person.Profession = null;
            person.Log("I got fired.");

            return true;
        }
    }
}
