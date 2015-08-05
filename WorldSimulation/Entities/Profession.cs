using WorldSimulation.Worlds.Professions;

namespace WorldSimulation.Entities
{
    public class Profession : DataEntity
    {
        public string Title { get; set; }
        public ProfessionScheduleEnum TimeUsage { get; set; }
        public int TimeUsageInHours { get;  set; }
        public string Company { get; set; }
        public Person Worker { get; set; }
        public bool IsAvailable { get { return Worker == null; } }

        public bool DoesMeetRequirements(Person person)
        {
            return true;
        }
    }
}
