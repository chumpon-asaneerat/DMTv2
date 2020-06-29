#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public abstract class NSearch
    {
        protected static object sync = new object();
    }

    public abstract class NSearch<T> : NSearch
        where T: NSearch, new()
    {
    }
}
