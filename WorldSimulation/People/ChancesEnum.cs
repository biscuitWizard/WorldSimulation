using System;

namespace WorldSimulation.People
{
    public enum ChancesEnum 
    {
        Impossible = 1000000,
        UnbelievablyRare = 999500,
        ExtremelyRare = 999000,
        VeryRare = 998500,
        Rare = 998000,
        Uncommon = 997500,
        Common = 997000,
        VeryCommon = 996500,
        ExtremelyCommon = 996000,
        UnbelievablyCommon = 995500,
        EveryoneHasIt = 0
    }

    public static class Chances
    {
        public static bool SuccessfulChance(this Random random, ChancesEnum chance)
        {
            return random.Next(0, 1000000) > (int)chance;
        }
    }
}
