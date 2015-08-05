namespace WorldSimulation.People
{
    /// <summary>
    /// Basic enum to provide some easily accessible values on the influence of
    /// certain facets on scoring.
    /// </summary>
    public enum FacetInfluenceEnum
    {
        None = 0,
        VeryMinor = 5,
        Minor = 10,
        Moderate = 20,
        Major = 50,
        Extreme = 75
    }

    public static class FacetInfluence
    {
        public static int ToScore(this FacetInfluenceEnum facetInfluence)
        {
            return (int) facetInfluence;
        }
    }
}
