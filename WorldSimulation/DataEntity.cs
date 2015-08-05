using System;
using System.Collections.Generic;
using System.Linq;

using WorldSimulation.Entities;
using WorldSimulation.Flags;

namespace WorldSimulation
{
    public abstract class DataEntity
    {
        public ulong? Id { get; set; }
        private readonly IDictionary<Flag, DateTime> _flags = new Dictionary<Flag, DateTime>();

        public void AddFlag(Flag flag)
        {
            _flags.Add(flag, Universe.CurrentUniverse.CurrentTime);
        }

        public bool HasFlag(Flag flag)
        {
            return _flags.Any(f => f.Equals(flag));
        }

        public Flag[] GetFlags(FlagCategory category)
        {
            return _flags.Keys.Where(f => f.Category.Equals(category)).ToArray();
        }

        public Flag[] GetFlags()
        {
            return _flags.Keys.ToArray();
        }

        public void ClearFlags(FlagCategory category)
        {
            var flags = _flags.Where(f => f.Key.Category.Equals(category)).ToArray();
            foreach (var flag in flags)
            {
                _flags.Remove(flag);
            }
        }

        public void ClearFlag(Flag flag)
        {
            _flags.Remove(_flags.FirstOrDefault(f => f.Equals(flag)));
        }

        public DateTime GetFlagCreationTime(Flag flag)
        {
            return _flags.FirstOrDefault(f => f.Key.Equals(flag)).Value;
        }
    }
}
