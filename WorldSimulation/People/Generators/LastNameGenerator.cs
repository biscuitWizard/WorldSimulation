using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorldSimulation.Entities;

namespace WorldSimulation.People.Generators
{
    public class LastNameGenerator : IPersonBuilder
    {
        private readonly IEnumerable<string> _names;

        public LastNameGenerator()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.Surnames.txt")))
            {
                _names = reader
                    .ReadToEnd()
                    .Split(new[] {'\n'})
                    .ToList();
            }
        }

        public Person Build(Person person)
        {
            string fname = "";
            if (person.Parents == null)
            {
                fname = _names
                    .OrderBy(name => Guid.NewGuid())
                    .First();
            }
            else
            {
                fname = person.Parents[0].FamilyName;
            }
            person.FamilyName = char.ToUpper(fname[0])
                                + fname.Substring(1);

            return person;
        }
    }
}