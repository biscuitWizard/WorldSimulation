using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Caches;
using WorldSimulation.Entities;
using WorldSimulation.People;

namespace WorldSimulation.Worlds
{
    public class Territory : DataEntity
    {
        public Territory Parent { get { return _parent; } }

        private readonly IProfessionCache _professionCache;
        private readonly IPersonCache _personCache;
        public string Name { get; set; }
        public string Category { get; set; }
        public bool SustainsLife { get; set; }
        public long CurrentPopulation { get { return _people.Count + _children.Select(c => c.CurrentPopulation).Sum(); } }
        private readonly IList<Territory> _children = new List<Territory>();
        private readonly Territory _parent;
        private readonly IList<ulong> _people = new List<ulong>();
        private readonly IList<ulong> _professions = new List<ulong>(); 

        public Territory(IProfessionCache professionCache,
            IPersonCache personCache,
            Territory parent = null)
        {
            _professionCache = professionCache;
            _personCache = personCache;
            _parent = parent;
        }

        public void AddTerritory(Territory child)
        {
            _children.Add(child);
        }

        public void AddProfession(Profession profession)
        {
            _professions.Add(profession.Id.Value);
        }

        public void MovePerson(Person person)
        {
            if (person.Location != null)
            {
                person.Location.RemovePerson(person);
            }

            _people.Add(person.Id.Value);
        }

        public void RemovePerson(Person person)
        {
            _people.Remove(person.Id.Value);
        }

        public Profession[] GetAvailableProfessions()
        {
            var professions = _professions.Select(id => _professionCache.Read(id)).Where(p => p.IsAvailable).ToArray();
            var childProfessions = _children.SelectMany(c => c.GetAvailableProfessions()).ToArray();

            return professions.Concat(childProfessions).ToArray();
        }

        public Person[] GetPeopleWhere(Func<Person, bool> predicate)
        {
            var people = _people.Select(p => _personCache.Read(p)).Where(predicate).ToArray();
            var childPeople = _children.SelectMany(c => c.GetPeopleWhere(predicate)).ToArray();

            return people.Concat(childPeople).ToArray();
        }

        public Territory[] GetLiveableTerritories()
        {
            var territories = new List<Territory>();
            if (SustainsLife)
            {
                territories.Add(this);
            }

            territories.AddRange(_children.SelectMany(c => c.GetLiveableTerritories()));

            return territories.ToArray();
        }

        public Territory[] GetTerritories()
        {
            return _children.ToArray();
        }
    }
}
