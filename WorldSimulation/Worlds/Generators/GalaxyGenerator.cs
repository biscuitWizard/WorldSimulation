using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorldSimulation.Caches;

namespace WorldSimulation.Worlds.Generators
{

    /// <summary>
    /// This is a special generator that can be used to generate a whole galaxy's worth of territories.
    /// 
    /// It will not generate people, just available locations.
    /// </summary>
    public class GalaxyGenerator
    {
        private readonly IPersonCache _personCache;
        private readonly IProfessionCache _professionCache;
        private readonly Random _random;
        private readonly string[] _planetNames;
        private readonly string[] _countryNames;
        private readonly string[] _stationNames;
        private readonly IList<string> _usedNames = new List<string>(); 

        public GalaxyGenerator(IPersonCache personCache, IProfessionCache professionCache, Random random)
        {
            _personCache = personCache;
            _professionCache = professionCache;
            _random = random;

            var assembly = Assembly.GetExecutingAssembly();
            using (
                var reader = new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.PlanetNames.txt"))
                )
            {
                _planetNames = reader.ReadToEnd()
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            }
            using (
                var reader =
                    new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.CountryNames.txt")))
            {
                _countryNames = reader.ReadToEnd()
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            }
            using (
                var reader =
                    new StreamReader(assembly.GetManifestResourceStream("WorldSimulation.Data.StationNames.txt")))
            {
                _stationNames = reader.ReadToEnd()
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Territory[] GeneratePlanets(Territory solarSystem, int count)
        {
            var planets = new List<Territory>();
            for (var i = 0; i < count; i++)
            {
                var planetCategory = GalaxyCategoryEnum.GasPlanet;
                if (_random.Next(0, 2) == 1)
                {
                    planetCategory = GalaxyCategoryEnum.WaterPlanet;
                }

                var newTerritory = new Territory(_professionCache, _personCache, solarSystem)
                {
                    Name = _planetNames.OrderBy(_ => Guid.NewGuid()).First(),
                    Category = planetCategory.ToString()
                };

                planets.Add(newTerritory);

                if (planetCategory == GalaxyCategoryEnum.GasPlanet)
                {
                    foreach (var station in GenerateSpaceStations(newTerritory, _random.Next(4, 10)))
                    {
                        newTerritory.AddTerritory(station);
                    }
                }
                else
                {
                    foreach (var territory in GenerateCountries(newTerritory, _random.Next(6, 15)))
                    {
                        newTerritory.AddTerritory(territory);
                    }
                }
            }

            return planets.ToArray();
        }

        public Territory[] GenerateCountries(Territory planet, int count)
        {
            var countries = new List<Territory>();
            for (var i = 0; i < count; i++)
            {
                var newCountry = new Territory(_professionCache, _personCache, planet)
                {
                    Name = _countryNames.OrderBy(_ => Guid.NewGuid()).First(),
                    Category = GalaxyCategoryEnum.Country.ToString(),
                    SustainsLife = true
                };

                while (_usedNames.Contains(newCountry.Name))
                {
                    newCountry.Name = _countryNames.OrderBy(_ => Guid.NewGuid()).First();
                }

                _usedNames.Add(newCountry.Name);

                countries.Add(newCountry);
            }

            return countries.ToArray();
        }

        public Territory[] GenerateSpaceStations(Territory planet, int count)
        {
            var stations = new List<Territory>();
            for (var i = 0; i < count; i++)
            {
                var newStation = new Territory(_professionCache, _personCache, planet)
                {
                    Name = _stationNames.OrderBy(_ => Guid.NewGuid()).First(),
                    Category = GalaxyCategoryEnum.SpaceStation.ToString(),
                    SustainsLife = true
                };

                while (_usedNames.Contains(newStation.Name))
                {
                    newStation.Name = _stationNames.OrderBy(_ => Guid.NewGuid()).First();
                }

                _usedNames.Add(newStation.Name);

                stations.Add(newStation);
            }

            return stations.ToArray();
        }

        public Territory[] GenerateSolarSystems(Territory galaxy, int count)
        {
            // Just a sample solar system!
            var solSystem = new Territory(_professionCache, _personCache, galaxy)
            {
                Name = "Sol System",
                Category = GalaxyCategoryEnum.SolarSystem.ToString()
            };

            foreach (var territory in GeneratePlanets(solSystem, _random.Next(4, 8)))
            {
                solSystem.AddTerritory(territory);
            }

            return new[] {solSystem};
        }

        public Territory GenerateGalaxy(int count = 1)
        {
            // We're just going to generate a sample galaxy with those code!
            var milkyWayGalaxy = new Territory(_professionCache, _personCache)
            {
                Name = "Milky Way Galaxy",
                Category = GalaxyCategoryEnum.Galaxy.ToString()
            };

            foreach (var territory in GenerateSolarSystems(milkyWayGalaxy, 1))
            {
                milkyWayGalaxy.AddTerritory(territory);
            }

            return milkyWayGalaxy;
        }
    }

    public enum GalaxyCategoryEnum
    {
        Galaxy,
        SolarSystem,
        WaterPlanet,
        GasPlanet,
        Country,
        SpaceStation
    }
}
