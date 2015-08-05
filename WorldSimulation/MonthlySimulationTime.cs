using System;

namespace WorldSimulation
{
    /// <summary>
    /// Every game tick is one month.
    /// </summary>
    public class MonthlySimulationTime : ISimulationTime
    {
        public DateTime CurrentTime { get; private set; }

        public MonthlySimulationTime(DateTime? startTime = null)
        {
            CurrentTime = startTime ?? DateTime.UtcNow;
        }
        
        public DateTime AdvanceTick()
        {
            return CurrentTime.AddMonths(1);
        }
    }
}
