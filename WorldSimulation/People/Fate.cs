using System;
using System.Collections.Generic;
using System.Linq;
using SharpNoise.Modules;

namespace WorldSimulation.People
{
    public class Fate
    {
        // Debug:
        public double[] FateNumbers;

        // 100 years' worth.
        public const int MonthsToGenerate = 1200;
        private const float Persistence = 128;
        private const int MonthOffset = 512;
        private const int MinimumPeaks = 2;

        private readonly int _seed;
        private readonly IEnumerable<int> _xAxis;
        public readonly IList<int> _peaks = new List<int>();
        public readonly IList<int> _valleys = new List<int>();
        

        private int _failedAttempts = 0;

        public Fate(int seed)
        {
            _seed = seed;
            _xAxis = Enumerable.Range(0, MonthsToGenerate);
        }

        public double[] CalculateLifeline()
        {
            try
            {

                var perlin = new Perlin {Lacunarity = 2.8, Persistence = .25, OctaveCount = 16, Seed = _seed};

                var lifeLine = _xAxis.Select(i => perlin.GetValue(i / 60f, i / 30f, i / 90f)).ToArray();
                PopulateEvents(lifeLine);

                if (_valleys.Count == 0)
                {
                    _failedAttempts++;
                    return CalculateLifeline();
                }

                // Debug
                FateNumbers = lifeLine;
                return lifeLine;
            }
            catch
            {
                return CalculateLifeline();
            }
        }

        protected virtual void PopulateEvents(double[] data)
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

        public Tuple<double, int>[] GetValleys(double[] data)
        {
            return data.WithNextAndPrevious()
                .Select((t, i) => Tuple.Create(t.Item1, t.Item2, t.Item3, i))
                .Where(t => t.Item2 < 0 && t.Item1 > t.Item2 && t.Item2 < t.Item3)
                .Select(t => Tuple.Create(t.Item2, t.Item4)).ToArray();
        }

        public Tuple<double, int>[] GetPeaks(double[] data)
        {
            return data.WithNextAndPrevious()
                .Select((t, i) => Tuple.Create(t.Item1, t.Item2, t.Item3, i))
                .Where(t => t.Item1 < t.Item2 & t.Item2 > t.Item3 && t.Item2 > 0)
                .Select(t => Tuple.Create(t.Item2, t.Item4)).ToArray();
        }
    }
}
