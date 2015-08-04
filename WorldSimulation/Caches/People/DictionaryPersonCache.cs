using System;
using System.Collections.Generic;
using System.Linq;
using WorldSimulation.Entities;
using WorldSimulation.People;

namespace WorldSimulation.Caches.People
{
    public class DictionaryPersonCache : BaseDictionaryCache<Person>, IPersonCache
    {
        public void MoveToGrave(Person person)
        {
            return;
        }

        public IList<Person> TakeRandom(int count)
        {
            return DataDictionary.OrderBy(_ => Guid.NewGuid()).Take(count).Select(kvp => kvp.Value).ToList();
        } 
    }
}
