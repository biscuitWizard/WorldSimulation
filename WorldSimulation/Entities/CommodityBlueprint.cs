using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSimulation.Entities
{
    /// <summary>
    /// A commodity blueprint is the master metadata set for a type of item.
    /// 
    /// All items that exist in the game have a base commodity blueprint that specifies
    /// certain global characteristics about them. For individual customizations to item,
    /// refer to CommodityInstances.
    /// </summary>
    public class CommodityBlueprint : DataEntity
    {
    }
}
