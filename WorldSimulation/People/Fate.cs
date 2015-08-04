using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorldSimulation.Entities;

namespace WorldSimulation.People
{
    public class Fate
    {
        // 100 years' worth.
        public const int MonthsToGenerate = 1200;
        private const float Persistence = 128;
        private const int MonthOffset = 512;
        private const int MinimumPeaks = 2;

        private readonly double _seed;
        private readonly Person _person;
        private readonly IEnumerable<int> _xAxis;
        public readonly IList<int> _peaks = new List<int>();
        public readonly IList<int> _valleys = new List<int>();
        

        private int _failedAttempts = 0;

        public Fate(double seed, Person person)
        {
            _seed = seed;
            _person = person;
            _xAxis = Enumerable.Range(0, MonthsToGenerate);
        }

        public Single[] CalculateLifeline()
        {
            try
            {
                var lifeline =
                    _xAxis.Select(
                        i =>
                            PerlinNoise1D.OctavePerlin(
                                (float) ((i + MonthOffset)*_seed)/(Persistence + _failedAttempts), 8,
                                .55f)).ToArray();
                PopulateEvents(lifeline);

                if (_valleys.Count == 0)
                {
                    _failedAttempts++;
                    return CalculateLifeline();
                }

                return lifeline;
            }
            catch
            {
                return CalculateLifeline();
            }
        }

        protected virtual void PopulateEvents(Single[] data)
        {
            _peaks.Clear();
            _valleys.Clear();

            var peaks = GetPeaks(data);
            foreach (var peak in peaks)
            {
                _peaks.Add(peak.Item2);
            }

            var valleys = GetValleys(data);
            foreach (var valley in valleys)
            {
                _valleys.Add(valley.Item2);
            }
        }

        public bool IsPeakDay(int months)
        {
            return _peaks.Contains(months);
        }

        public Tuple<float, int>[] GetValleys(Single[] data)
        {
            return data.WithNextAndPrevious()
                .Select((t, i) => Tuple.Create(t.Item1, t.Item2, t.Item3, i))
                .Where(t => t.Item2 < 0 && t.Item1 > t.Item2 && t.Item2 < t.Item3)
                .Select(t => Tuple.Create(t.Item2, t.Item4)).ToArray();
        }

        public Tuple<float, int>[] GetPeaks(Single[] data)
        {
            return data.WithNextAndPrevious()
                .Select((t, i) => Tuple.Create(t.Item1, t.Item2, t.Item3, i))
                .Where(t => t.Item1 < t.Item2 & t.Item2 > t.Item3 && t.Item2 > 0)
                .Select(t => Tuple.Create(t.Item2, t.Item4)).ToArray();
        }
    }
}
