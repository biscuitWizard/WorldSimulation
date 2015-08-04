using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldSimulation.Caches
{
    public abstract class BaseDictionaryCache<T> : IDataEntityCache<T> where T : DataEntity
    {
        protected IDictionary<ulong, T> DataDictionary { get { return _data; } } 

        private readonly IDictionary<ulong, T> _data = new Dictionary<ulong, T>();
        private ulong _lastAssignedId;
        private readonly object _idLock = new object();

        public T Save(T entity)
        {
            if (!entity.Id.HasValue)
            {
                entity.Id = GetNextAvailableId();
            }

            _data.Add(entity.Id.Value, entity);

            return entity;
        }

        public T Read(ulong id)
        {
            return _data.FirstOrDefault(p => p.Key == id).Value;
        }

        public IList<T> ReadWhere(Func<T, bool> predicate)
        {
            return _data.Select(kvp => kvp.Value).Where(predicate).ToList();
        }

        protected virtual ulong GetNextAvailableId()
        {
            lock (_idLock)
            {
                _lastAssignedId++;
                return _lastAssignedId;
            }
        }
    }
}
