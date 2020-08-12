﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

using DMT.Models;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage common data on Revenue Entry.
    /// </summary>
    public class RevenueController : ApiController
    {
        #region UserShiftRevenue

        [HttpPost]
        [ActionName(RouteConsts.Revenue.CreatePlazaRevenue.Name)]
        public NDbResult<UserShiftRevenue> CreateRevenueShift([FromBody] Search.Revenues.PlazaShift value)
        {
            NDbResult<UserShiftRevenue> result;
            if (null == value)
            {
                result = new NDbResult<UserShiftRevenue>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = UserShiftRevenue.CreatePlazaRevenue(value.Shift, value.PlazaGroup);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Revenue.SavePlazaRevenue.Name)]
        public NDbResult<UserShiftRevenue> SaveRevenueShift([FromBody] Search.Revenues.SaveRevenueShift value)
        {
            NDbResult<UserShiftRevenue> result;
            if (null == value)
            {
                result = new NDbResult<UserShiftRevenue>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = UserShiftRevenue.SavePlazaRevenue(
                    value.RevenueShift, value.RevenueDate, value.RevenueId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Revenue.GetPlazaRevenue.Name)]
        public NDbResult<UserShiftRevenue> GetRevenueShift([FromBody] Search.Revenues.PlazaShift value)
        {
            NDbResult<UserShiftRevenue> result;
            if (null == value)
            {
                result = new NDbResult<UserShiftRevenue>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = UserShiftRevenue.GetPlazaRevenue(value.Shift, value.PlazaGroup);
            }
            return result;
        }

        #endregion

        #region Revenue Entry

        [HttpPost]
        [ActionName(RouteConsts.Revenue.SaveRevenue.Name)]
        public NDbResult<RevenueEntry> SaveRevenue([FromBody] RevenueEntry value)
        {
            NDbResult<RevenueEntry> result;
            if (null == value)
            {
                result = new NDbResult<RevenueEntry>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
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
                result = RevenueEntry.Save(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Revenue.GetRevenues.Name)]
        public NDbResult<List<RevenueEntry>> GetRevenues([FromBody] DateTime value)
        {
            NDbResult<List<RevenueEntry>> result;

            if (value == DateTime.MinValue)
            {
                result = new NDbResult<List<RevenueEntry>>();
                result.ParameterIsNull();
                result.data = new List<RevenueEntry>();
            }
            else
            {
                result = RevenueEntry.FindByRevnueDate(value);
            }

            return result;
        }

        #endregion
    }
}
