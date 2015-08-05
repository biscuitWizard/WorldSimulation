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
                new Facet("Openness To Experience", FacetTypes.InventiveOrCurious, FacetTypes.ConsistentOrCautious),
                new Facet("Conscientiousness", FacetTypes.EfficientOrOrganized, FacetTypes.EasyGoingOrCareless),
                new Facet("Extraversion", FacetTypes.FriendlyOrCompassionate, FacetTypes.SolitaryOrReserved),
                new Facet("Agreeableness", FacetTypes.FriendlyOrCompassionate, FacetTypes.AnalyticalOrDetached),
                new Facet("Neuroticism", FacetTypes.SensitiveOrNervous, FacetTypes.SecureOrConfident)
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
        public FacetTypes AntiPole { get; private set; }
        public FacetTypes Pole { get; private set; }
        public FacetTypes DominantPole { get; private set; }
        public int Value { get; set; }

        public Facet(string name, FacetTypes pole, FacetTypes antipole, int value = 0)
        {
            Name = name;
            Pole = pole;
            AntiPole = antipole;
            Value = value;
        }
    }

    public enum FacetTypes
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
