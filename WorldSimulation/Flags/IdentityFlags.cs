namespace WorldSimulation.Flags
{
    public static class IdentityFlags
    {
        public static Flag TransgenderFlag = Flag.Create("Transgender", FlagCategory.Identity);
        public static Flag TransitioningFlag = Flag.Create("Transitioning", FlagCategory.Identity);
        public static Flag TransitionedFlag = Flag.Create("Transitioned", FlagCategory.Identity);
        public static Flag OrphanFlag = Flag.Create("Orphan", FlagCategory.Identity);
    }
}
