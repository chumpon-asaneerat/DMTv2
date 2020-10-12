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
            var results = Shift.GetShifts();
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
        [ActionName(RouteConsts.Shift.Create.Name)]
        public NDbResult<TSBShift> Create([FromBody] TSBShiftCreate value)
        {
            NDbResult<TSBShift> result;
            if (null == value)
            {
                result = new NDbResult<TSBShift>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBShift.Create(value.Shift, value.User);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Shift.GetCurrent.Name)]
        public NDbResult<TSBShift> GetCurrent()
        {
            var result = TSBShift.GetTSBShift();
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
                    var plaza = ConfigManager.Instance.Plaza;
                    if (null != plaza)
                    {
                        var taApp = plaza.TAApp;
                        var todApp = plaza.TODApp;
                        if (null != taApp && null != taApp.Http)
                        {
                            var http = taApp.Http;
                            NRestClient.WebProtocol protocol = (http.Protocol == "http") ?
                                NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                            string hostName = http.HostName;
                            int portNo = http.PortNumber;
                            var client = new NRestClient(protocol, hostName, portNo);
                            if (null != client)
                            {
                                client.Execute2<NRestResult>(RouteConsts.Notify.ShiftChanged.Url, new { });
                            }
                        }
                        if (null != todApp && null != todApp.Http)
                        {
                            var http = todApp.Http;
                            NRestClient.WebProtocol protocol = (http.Protocol == "http") ?
                                NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                            string hostName = http.HostName;
                            int portNo = http.PortNumber;
                            var client = new NRestClient(protocol, hostName, portNo);
                            if (null != client)
                            {
                                client.Execute2<NRestResult>(RouteConsts.Notify.ShiftChanged.Url, new { });
                            }
                        }
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
