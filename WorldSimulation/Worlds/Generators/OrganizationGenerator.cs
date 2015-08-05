using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Caches;

namespace WorldSimulation.Worlds.Generators
{
    public class OrganizationGenerator
    {
        private readonly ProfessionGenerator _professionGenerator;
        private readonly IProfessionCache _professionCache;

        public OrganizationGenerator(ProfessionGenerator professionGenerator,
            IProfessionCache professionCache)
        {
            _professionGenerator = professionGenerator;
            _professionCache = professionCache;
        }

        /// <summary>
        /// Generates the organizations for the indicated root territory. All territories
        /// capable of sustaining life will be considered viable territories
        /// for organizations to be spawned in.
        /// </summary>
        /// <param name="rootTerritory">The root territory.</param>
        /// <param name="random">The random.</param>
        public void GenerateOrganizations(Territory rootTerritory, Random random, int count = 300)
        {
            var professions =
                _professionGenerator.GenerateProfessions(count).Select(p => _professionCache.Save(p)).ToArray();
            var territories = rootTerritory.GetLiveableTerritories();

            foreach (var company in professions.Select(p => p.Company).Distinct())
            {
                var selectedTerritory = territories.OrderBy(_ => Guid.NewGuid()).First();
                foreach (var profession in professions.Where(p => p.Company.Equals(company)))
                {
                    selectedTerritory.AddProfession(profession);
                }
            }
        }
    }
}
