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
        #region Shift

        [HttpPost]
        [ActionName(RouteConsts.Shift.GetShifts.Name)]
        public NDbResult<List<Shift>> GetShifts()
        {
            var results = Shift.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.SaveShift.Name)]
        public NDbResult<Shift> SaveShift([FromBody] Shift value)
        {
            NDbResult<Shift> result;
            if (null == value)
            {
                result = new NDbResult<Shift>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Shift.Save(value);
            }
            return result;
        }

        #endregion

        #region TSB Shift

        [HttpPost]
        [ActionName(RouteConsts.Shift.GetCurrent.Name)]
        public NDbResult<TSBShift> GetCurrent()
        {
            var result = TSBShift.GetCurrent();
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.ChangeShift.Name)]
        public NDbResult ChangeShift([FromBody] TSBShift value)
        {
            NDbResult result;
            if (null == value)
            {
                result = new NDbResult<Shift>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBShift.ChangeShift(value);
                if (!result.errors.hasError)
                {
                    // Raise event.
                    LocalDbServer.Instance.ChangeShift();
                }
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.Create.Name)]
        public NDbResult<TSBShift> Create([FromBody] TSBShiftCreate value)
        {
            NDbResult<TSBShift> result;
            if (null == value)
            {
                result = new NDbResult<TSBShift>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = TSBShift.Create(value.Shift, value.User);
            }
            return result;
        }

        #endregion
    }
}
