using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WorldSimulation.Caches;

namespace WorldSimulation.Items
{
    public class ItemModule : IModule
    {
        private readonly ICommodityCache _commodityCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemModule"/> class.
        /// </summary>
        /// <param name="commodityCache">The commodity cache.</param>
        public ItemModule(ICommodityCache commodityCache)
        {
            _commodityCache = commodityCache;
        }

        public void Setup()
        {
            throw new NotImplementedException();
        }
    }
}
