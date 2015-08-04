//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using ServiceStack.Redis;
//using WorldSimulation.People;

//namespace WorldSimulation.Caches.People
//{
//    /// <summary>
//    /// Encapsulates some basic functionality used around storing and getting people.
//    /// 
//    /// This has to be implemented because so many people are generated that it takes forever for the
//    /// program to process them all.
//    /// 
//    /// This will hopefully accelerate that.
//    /// </summary>
//    public class RedisPersonCache : IPersonCache
//    {
//        private readonly IRedisClientsManager _clientsManager;
//        private ulong _lastAssignedId;
//        private readonly object _idLock = new object();

//        public RedisPersonCache(IRedisClientsManager clientsManager)
//        {
//            _clientsManager = clientsManager;
//        }

//        public Person Save(Person entity)
//        {
//            if (!entity.Id.HasValue)
//            {
//                lock (_idLock)
//                {
//                    _lastAssignedId++;
//                    entity.Id = _lastAssignedId;
//                }
//            }

//            using (var client = _clientsManager.GetClient())
//            {
//                client.Store(entity);
//            }

//            return entity;
//        }

//        public Person Read(ulong id)
//        {
//            var results = ReadWhere(p => p.Id == id);

//            if (results.Count > 1)
//            {
//                throw new InvalidDataException("Oops! There are multiple people with the same ID. What?");
//            }

//            if (!results.Any())
//            {
//                throw new InvalidOperationException("No person found by that ID.");
//            }

//            return results.First();
//        }

//        public IList<Person> ReadWhere(Func<Person, bool> predicate)
//        {
//            using (var client = _clientsManager.GetClient())
//            {
//                var results = client.As<Person>().GetAll().Where(predicate).ToList();

//                return results;
//            }
//        }

//        /// <summary>
//        /// Moves a person to the Redis storage solution.
//        /// </summary>
//        /// <param name="person">The person.</param>
//        public void MoveToGrave(Person person)
//        {
            
//        }
//    }
//}
