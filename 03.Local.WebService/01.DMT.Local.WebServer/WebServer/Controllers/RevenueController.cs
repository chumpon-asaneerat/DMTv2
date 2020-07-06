#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
using DMT.Models.ExtensionMethods;
using NLib.Controls.Utils;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage common data on Revenue Entry.
    /// </summary>
    public class RevenueController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Revenue.CreatePlazaRevenue.Name)]
        public UserShiftRevenue CreateRevenueShift([FromBody] Search.Revenues.PlazaShift value)
        {
            if (null == value) return null;
            return UserShiftRevenue.CreatePlazaRevenue(value.Shift, value.Plaza);
        }
        [HttpPost]
        [ActionName(RouteConsts.Revenue.GetPlazaRevenue.Name)]
        public UserShiftRevenue GetRevenueShift([FromBody] Search.Revenues.PlazaShift value)
        {
            if (null == value) return null;
            return UserShiftRevenue.GetPlazaRevenue(value.Shift, value.Plaza);
        }
        [HttpPost]
        [ActionName(RouteConsts.Revenue.SavePlazaRevenue.Name)]
        public void SaveRevenueShift([FromBody] Search.Revenues.SaveRevenueShift value)
        {
            if (null == value) return;
            UserShiftRevenue.SavePlazaRevenue(value.RevenueShift, value.RevenueDate, value.RevenueId);
        }
        [HttpPost]
        [ActionName(RouteConsts.Revenue.SaveRevenue.Name)]
        public void SaveRevenue([FromBody] RevenueEntry value)
        {
            if (null == value) return;
            if (value.PKId == Guid.Empty)
            {
                value.PKId = Guid.NewGuid();
            }
            if (value.RevenueId == string.Empty)
            {
                Random rand = new Random();
                if (string.IsNullOrWhiteSpace(value.RevenueId))
                {
                    value.RevenueId = rand.Next(100000).ToString("D5"); // auto generate.
                }
            }
            RevenueEntry.Save(value);
        }
    }
}
