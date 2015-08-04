using System;

namespace WorldSimulation.People
{
    public class Timeline
    {
        public DateTime CurrentDate { get { return _currentTime; } }

        private const int MonthsPerYear = 12;
        private readonly DateTime _startTime;
        private DateTime _currentTime;

        public int Month { get { return _currentTime.Month; } }
        public int Year { get { return _currentTime.Year; } }

        public Timeline(DateTime startTime)
        {
            _startTime = startTime;
            _currentTime = startTime;
        }

        public event EventHandler MonthElapsed;

        public int GetElapsedYears()
        {
            return _startTime.Year - _currentTime.Year;
        }

        public void AddYears(int years)
        {
            AddMonths(years * MonthsPerYear);
        }

        public void AddMonths(int months)
        {
            for (var i = 0; i < months; i++)
            {
                _currentTime = _currentTime.AddMonths(1);
                if (MonthElapsed != null)
                {
                    MonthElapsed.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}