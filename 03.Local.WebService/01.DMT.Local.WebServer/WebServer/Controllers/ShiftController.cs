#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage Shift and TSBShift (Supervisor shift).
    /// </summary>
    public class ShiftController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Shift.GetShifts.Name)]
        public List<Shift> GetShifts()
        {
            return Shift.Gets();
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.GetCurrent.Name)]
        public TSBShift GetCurrent()
        {
            return TSBShift.GetCurrent();
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.ChangeShift.Name)]
        public void ChangeShift([FromBody] TSBShift shift)
        {
            TSBShift.ChangeShift(shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.Create.Name)]
        public TSBShift Create([FromBody] TSBShiftCreate value)
        {
            if (null == value) return null;
            return TSBShift.Create(value.Shift, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.SaveShift.Name)]
        public Shift SaveShift([FromBody] Shift value)
        {
            if (null != value)
            {
                Shift.Save(value);
            }
            return value;
        }
    }
}
