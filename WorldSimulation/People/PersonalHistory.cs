using System.Collections.Generic;
using WorldSimulation.Entities;

namespace WorldSimulation.People
{
    public class PersonalHistory
    {
        public IList<Person> Divorces { get; set; }
        public List<string> Log { get; set; }
        public IList<Profession> PastJobs { get; set; }
        public IList<string> Schools { get; set; }

        private readonly Person _owner;

        public PersonalHistory(Person owner)
        {
            _owner = owner;
            Divorces = new List<Person>();
            Log = new List<string>();
            PastJobs = new List<Profession>();
            Schools = new List<string>();
        }
    }
}
