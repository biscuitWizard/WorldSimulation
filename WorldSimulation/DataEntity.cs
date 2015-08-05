using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Flags;

namespace WorldSimulation
{
    public abstract class DataEntity
    {
        public ulong? Id { get; set; }
        private readonly IList<Flag> _flags = new List<Flag>();

        public void AddFlag(Flag flag)
        {
            _flags.Add(flag);
        }

        public bool HasFlag(Flag flag)
        {
            return _flags.Any(f => f.Equals(flag));
        }

        public Flag[] GetFlags(FlagCategory category)
        {
            return _flags.Where(f => f.Category.Equals(category)).ToArray();
        }

        public Flag[] GetFlags()
        {
            return _flags.ToArray();
        }

        public void ClearFlags(FlagCategory category)
        {
            var flags = _flags.Where(f => f.Category.Equals(category)).ToArray();
            foreach (var flag in flags)
            {
                _flags.Remove(flag);
            }
        }

        public void ClearFlag(Flag flag)
        {
            _flags.Remove(_flags.FirstOrDefault(f => f.Equals(flag)));
        }
    }
}
