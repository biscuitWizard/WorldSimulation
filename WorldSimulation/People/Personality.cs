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
                new Facet("Openness To Experience", FacetType.InventiveOrCurious, FacetType.ConsistentOrCautious),
                new Facet("Conscientiousness", FacetType.EfficientOrOrganized, FacetType.EasyGoingOrCareless),
                new Facet("Extraversion", FacetType.FriendlyOrCompassionate, FacetType.SolitaryOrReserved),
                new Facet("Agreeableness", FacetType.FriendlyOrCompassionate, FacetType.AnalyticalOrDetached),
                new Facet("Neuroticism", FacetType.SensitiveOrNervous, FacetType.SecureOrConfident)
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
    }

    public class Facet
    {
        public string Name { get; private set; }
        public FacetType AntiPole { get; private set; }
        public FacetType Pole { get; private set; }
        public FacetType DominantPole { get; private set; }
        public int Value { get; set; }

        public Facet(string name, FacetType pole, FacetType antipole, int value = 0)
        {
            Name = name;
            Pole = pole;
            AntiPole = antipole;
            Value = value;
        }
    }

    public enum FacetType
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
