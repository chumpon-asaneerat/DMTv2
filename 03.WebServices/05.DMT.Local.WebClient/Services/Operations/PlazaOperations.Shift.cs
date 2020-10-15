#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private ShiftOperations _Shift_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Shift Operations.
        /// </summary>
        public ShiftOperations Shifts
        {
            get
            {
                if (null == _Shift_Ops)
                {
                    lock (this)
                    {
                        _Shift_Ops = new ShiftOperations();
                    }
                }
                return _Shift_Ops;
            }
        }

        #endregion

        #region ShiftOperations (Supervisor Shift)

        /// <summary>
        /// The ShiftOperations class.
        /// Used for Manage Supervisor's Shift operation(s).
        /// </summary>
        public class ShiftOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ShiftOperations() { }

            #endregion

            #region Public Methods

            #region Shift

            public NRestResult<List<Shift>> GetShifts()
            {
                NRestResult<List<Shift>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Shift>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<Shift>>(RouteConsts.Shift.GetShifts.Url, new { });
                return ret;
            }

            public NRestResult<Shift> SaveShift(Shift value)
            {
                NRestResult<Shift> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<Shift>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<Shift>(RouteConsts.Shift.SaveShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<Shift>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region TSB Shift

            public NRestResult<TSBShift> Create(Shift shift, User supervisor)
            {
                NRestResult<TSBShift> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBShift>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                var inst = new TSBShiftCreate()
                {
                    Shift = shift,
                    User = supervisor
                };
                ret = client.Execute<TSBShift>(RouteConsts.Shift.Create.Url, inst);
                return ret;
            }

            public NRestResult<TSBShift> GetCurrent()
            {
                NRestResult<TSBShift> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBShift>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<TSBShift>(RouteConsts.Shift.GetCurrent.Url, new { });
                return ret;
            }

            public NRestResult ChangeShift(TSBShift value, List<Plaza> plazas)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    // set date
                    value.Begin = DateTime.Now;
                    ret = client.Execute(RouteConsts.Shift.ChangeShift.Url, value);
                    if (ret.Ok && null != plazas && plazas.Count > 0)
                    {
                        // send to server
                        SCWOperations server = SCWServiceOperations.Instance.Plaza;
                        var inst = new SCWChiefOfDuty();
                        inst.networkId = 31; // TODO: network id required.
                        inst.plazaId = Convert.ToInt32(plazas[0].SCWPlazaId);
                        inst.staffId = value.UserId;
                        inst.staffTypeId = 1;
                        inst.beginDateTime = value.Begin;
                        // send.
                        server.TOD.SaveChiefOfDuty(inst);
                    }
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
