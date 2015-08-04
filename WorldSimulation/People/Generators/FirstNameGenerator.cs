using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorldSimulation.Entities;

namespace WorldSimulation.People.Generators
{
    public class FirstNameGenerator : IPersonBuilder
    {
        private readonly IEnumerable<string> _femaleNames;
        private readonly IEnumerable<string> _maleNames;

        public FirstNameGenerator()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.FemaleNames.txt")))
            {
                _femaleNames = reader
                    .ReadToEnd()
                    .Split(new[] {'\n'})
                    .Select(line => line
                        .Split(new[] {' '})
                        .First())
                    .Where(line => !String.IsNullOrEmpty(line))
                    .ToList();
            }

            using (var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.MaleNames.txt")))
            {
                _maleNames = reader
                    .ReadToEnd()
                    .Split(new[] {'\n'})
                    .Select(line => line
                        .Split(new[] {' '})
                        .First())
                    .Where(line => !String.IsNullOrEmpty(line))
                    .ToList();
            }
        }

        public Person Build(Person person)
        {
            string fname = "";
            if (person.Gender == "Female")
                fname = _femaleNames
                    .OrderBy(name => Guid.NewGuid())
                    .First()
                    .ToLower();
            else
                fname = _maleNames
                    .OrderBy(name => Guid.NewGuid())
                    .First()
                    .ToLower();
            person.FirstName = char.ToUpper(fname[0])
                               + fname.Substring(1);

            return person;
        }
    }
}