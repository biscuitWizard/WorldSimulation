using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.Professions
{
    public class FiredLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return person.Profession != null;
        }

        public bool Encounter(Person person)
        {
            person.Profession = null;
            person.Log("I got fired.");

            return true;
        }
    }
}
