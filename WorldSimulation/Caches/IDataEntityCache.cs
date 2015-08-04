using System;
using System.Collections.Generic;

namespace WorldSimulation.Caches
{
    public interface IDataEntityCache<T> where T : DataEntity
    {
        T Save(T entity);
        T Read(ulong id);
        IList<T> ReadWhere(Func<T, bool> predicate);
    }
}
