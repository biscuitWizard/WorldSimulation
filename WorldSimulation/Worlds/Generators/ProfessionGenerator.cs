using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorldSimulation.Entities;
using WorldSimulation.World.Professions;

namespace WorldSimulation.Worlds.Generators
{
    public class ProfessionGenerator
    {
        private readonly Random _random;
        private static IList<string> _titleNames;
        private static IList<string> _companyNames; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfessionGenerator"/> class.
        /// </summary>
        public ProfessionGenerator(Random random)
        {
            _random = random;
            var assembly = Assembly.GetExecutingAssembly();
            using (
                var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.Professions.txt"))
                )
            {
                _titleNames = reader.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            }

            using (
                var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.CompanyNames.txt")))
            {
                _companyNames = reader.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None); 
            }
        }

        public Profession GenerateProfession(string company = null)
        {
            if (!_titleNames.Any()
                || !_companyNames.Any())
            {
                throw new InvalidOperationException("Ran out of profession and/or company names!");
            }

            var schedule = 
                ((ProfessionScheduleEnum[]) Enum.GetValues(typeof (ProfessionScheduleEnum))).OrderBy(_ => Guid.NewGuid())
                    .First();

            var hours = 0;
            switch (schedule)
            {
                case ProfessionScheduleEnum.FullTime:
                    hours = 8;
                    break;
                case ProfessionScheduleEnum.PartTime:
                    hours = _random.Next(15, 35) / 7;
                    break;
                case ProfessionScheduleEnum.Overtime:
                    hours = _random.Next(45, 60) / 7;
                    break;
            }

            return new Profession
            {
                Title = _titleNames.OrderBy(_ => Guid.NewGuid()).First(),
                Company = company ?? _companyNames.OrderBy(_ => Guid.NewGuid()).First(),
                TimeUsage = schedule,
                TimeUsageInHours = hours
            };
        }

        public IList<Profession> GenerateProfessions(int count, string companyName = null)
        {
            var professions = new List<Profession>();
            for (var i = 0; i < count; i++)
            {
                professions.Add(GenerateProfession(companyName));
            }

            return professions;
        }
    }
}
