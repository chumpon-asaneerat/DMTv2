#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;

#endregion

namespace DMT.Services
{
    public class ShiftController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Shift.GetShifts.Name)]
        public List<Shift> GetShifts()
        {
            var results = Shift.Gets();
            return results;
        }
    }
}
