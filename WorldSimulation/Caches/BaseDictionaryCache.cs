using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WorldSimulation.Caches
{
    public abstract class BaseDictionaryCache<T> : IDataEntityCache<T> where T : DataEntity
    {
        protected IDictionary<ulong, T> DataDictionary { get { return _data; } } 

        private readonly IDictionary<ulong, T> _data = new Dictionary<ulong, T>();
        private ulong _lastAssignedId;
        private readonly object _idLock = new object();
        private readonly PropertyInfo[] _propertyInfo;

        protected BaseDictionaryCache()
        {
            _propertyInfo =
                typeof(T).GetProperties().Where(p => typeof(IEnumerable<T>).IsAssignableFrom(p.PropertyType)).ToArray();
        }

        public T Save(T entity)
        {
            // Save all constituents.
            foreach (var property in _propertyInfo)
            {
                foreach (T constituent in (IEnumerable<T>)property.GetValue(entity))
                {
                    if (!_data.ContainsKey(constituent.Id.Value))
                    {
                        _data.Add(constituent.Id.Value, constituent);
                    }
                    else
                    {
                        // Save!
                        _data[constituent.Id.Value] = constituent;
                    }
                }
            }

            if (entity.Id.HasValue && _data.ContainsKey(entity.Id.Value))
            {
                // Modify and update
                _data[entity.Id.Value] = entity;
                
                return entity;
            }

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
