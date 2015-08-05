using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldSimulation.People
{
    public class Personality
    {
        public Facet[] PersonalityFacets { get { return _facets.ToArray(); } } 

        private readonly List<Facet> _facets = new List<Facet>();

        protected Personality(IEnumerable<Facet> facets)
        {
            _facets.AddRange(facets);
        }

        public static Personality CreateRandom(Random random)
        {
            var personality = Create();

            foreach (var facet in personality._facets)
            {
                facet.Value = random.Next(-10, 10);
            }

            return personality;
        }

        public static Personality Create()
        {
            var facets = new []
            {
                new Facet("Openness To Experience", FacetTypeEnum.InventiveOrCurious, FacetTypeEnum.ConsistentOrCautious),
                new Facet("Conscientiousness", FacetTypeEnum.EfficientOrOrganized, FacetTypeEnum.EasyGoingOrCareless),
                new Facet("Extraversion", FacetTypeEnum.FriendlyOrCompassionate, FacetTypeEnum.SolitaryOrReserved),
                new Facet("Agreeableness", FacetTypeEnum.FriendlyOrCompassionate, FacetTypeEnum.AnalyticalOrDetached),
                new Facet("Neuroticism", FacetTypeEnum.SensitiveOrNervous, FacetTypeEnum.SecureOrConfident)
            };

            return new Personality(facets);
        }

        public string[] GetFacets()
        {
            return _facets.Select(f => f.Name).ToArray();
        }

        public int GetFacetValue(string facetName)
        {
            return _facets.First(f => f.Name.Equals(facetName)).Value;
        }

        public FacetTypeEnum GetDominantFacetType(string facetName)
        {
            return _facets.First(f => f.Name.Equals(facetName)).DominantPole;
        }

        public Facet GetFacet(FacetTypeEnum derivativeTypeEnum)
        {
            return _facets.FirstOrDefault(facet => facet.AntiPole == derivativeTypeEnum || facet.Pole == derivativeTypeEnum);
        }
    }

    public class Facet
    {
        public string Name { get; private set; }
        public FacetTypeEnum AntiPole { get; private set; }
        public FacetTypeEnum Pole { get; private set; }
        public FacetTypeEnum DominantPole { get; private set; }
        public int Value { get; set; }

        public Facet(string name, FacetTypeEnum pole, FacetTypeEnum antipole, int value = 0)
        {
            Name = name;
            Pole = pole;
            AntiPole = antipole;
            Value = value;
        }
    }

    public enum FacetTypeEnum
    {
        InventiveOrCurious,
        ConsistentOrCautious,
        EfficientOrOrganized,
        EasyGoingOrCareless,
        FriendlyOrCompassionate,
        AnalyticalOrDetached,
        SolitaryOrReserved,
        SensitiveOrNervous,
        SecureOrConfident
    }
}
