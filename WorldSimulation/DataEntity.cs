using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Flags;

namespace WorldSimulation
{
    public abstract class DataEntity
    {
        public ulong? Id { get; set; }
        private readonly IList<Flag> _flags = new List<Flag>();

        public void AddFlag(string name, bool value = true)
        {
            _flags.Add(Flag.Create(name, value));
        }

        public void AddFlag(Flag flag)
        {
            _flags.Add(flag);
        }

        public void SetFlagValue(string flagName, bool value)
        {
            var flag = GetFlag(flagName);
            if (flag == null)
            {
                throw new InvalidOperationException("Unable to locate flag.");
            }

            flag.Value = value;
        }

        public void RemoveFlag(string flagName)
        {
            var flag = GetFlag(flagName);
            if (flag == null)
            {
                throw new InvalidOperationException("Unable to locate flag.");
            }

            _flags.Remove(flag);
        }

        public void RemoveFlag(Flag flag)
        {
            var flagToRemove = _flags.FirstOrDefault(f => f.Equals(flag));

            if (flagToRemove == null)
            {
                throw new InvalidOperationException("Unable to locate flag");
            }

            _flags.Remove(flagToRemove);
        }

        private Flag GetFlag(string name)
        {
            return _flags.FirstOrDefault(f => f.Name.EqualsIgnoreCase(name));
        }

        public bool HasFlag(string name, bool expectedValue = true)
        {
            var flag = GetFlag(name);

            return flag != null && flag.Value == expectedValue;
        }

        public bool HasFlag(Flag flag)
        {
            return _flags.Any(f => f.Equals(flag));
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
    }
}
