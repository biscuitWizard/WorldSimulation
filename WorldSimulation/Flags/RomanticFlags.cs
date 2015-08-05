namespace WorldSimulation.Flags
{
    public static class RomanticFlags
    {
        public static Flag DatingFlag = Flag.Create("Dating", true, FlagCategory.Romantic);
        public static Flag EngagedFlag = Flag.Create("Engaged", true, FlagCategory.Romantic);
        public static Flag MarriedFlag = Flag.Create("Married", true, FlagCategory.Romantic);
    }
}
