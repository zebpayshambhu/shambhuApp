using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer
{
    public class EnumUtility
    {
        public enum DBAction
        {
            Insert = 1,
            Update = 2
        }

        public enum DBStatus
        {
            Inactive = 0,
            Active = 1
        }
    }
}
