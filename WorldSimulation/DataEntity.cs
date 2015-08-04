using System;
using System.Collections.Generic;
using System.Linq;

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

        private Flag GetFlag(string name)
        {
            return _flags.FirstOrDefault(f => f.Name.EqualsIgnoreCase(name));
        }

        public bool HasFlag(string name, bool expectedValue = true)
        {
            var flag = GetFlag(name);

            return flag != null && flag.Value == expectedValue;
        }
    }
}
