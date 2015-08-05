using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.People;
using WorldSimulation.Worlds;

namespace WorldSimulation.Entities
{
    public class Person : DataEntity
    {
        public int Age { get { return (int)(_timeline.CurrentDate - BirthDate).TotalDays / 365; } }
        public string Sex { get; set; }
        public string Gender { get; set; }
        public Profession Profession { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Person Partner { get; set; }
        public IList<Person> Children { get; set; }
        public Population Population { get; set; }
        public Territory Location { get; set; }
        public Personality Personality { get; set; }

        public bool Deceased
        {
            get { return DeathDate != null; }
        }

        public Fate Fate { get; set; }

        public Person[] Parents { get; set; }

        public string Name { get { return string.Format("{0} {1}", FirstName, FamilyName); } }

        public string FirstName
        {
            get { return _firstNames[_firstNames.Count - 1]; }
            set { _firstNames.Add(value); }
        }

        public string FamilyName
        {
            get { return _lastNames[_lastNames.Count - 1]; }
            set { _lastNames.Add(value); }
        }

        public string GetPronoun()
        {
            return Gender == "Female"
                ? "She"
                : "He";
        }

        public string GetOriginalFirstName()
        {
            return _firstNames[0];
        }

        public string GetOriginalFamilyName()
        {
            return _lastNames[0];
        }

        public string GetOriginalFullName()
        {
            return _firstNames[0]
                   + " " + _lastNames[0];
        }

        public PersonalHistory History { get { return _history; } }

        private readonly IList<string> _firstNames
            = new List<string>();

        private readonly IList<string> _lastNames
            = new List<string>();

        private readonly PersonalHistory _history;
        private readonly Timeline _timeline;

        public Person(Timeline timeline)
        {
            _timeline = timeline;
            Children = new List<Person>();
            _history = new PersonalHistory(this);
        }

        public void Log(string message, params object[] arguments)
        {
            if (arguments.Any())
            {
                _history.Log.Add(string.Format(message, arguments));
            }
            else
            {
                _history.Log.Add(message);
            }

        }

        public bool IsMajorEventDate(int months)
        {
            return Fate.IsPeakDay(months) || Fate.IsValleyDay(months);
        }
    }
}