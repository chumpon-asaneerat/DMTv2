#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
using NLib;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for Notify event to application.
    /// </summary>
    //[Authorize]
    public class NotifyController : ApiController
    {
        #region ActiveChanged

        /// <summary>
        /// Call when set active TSB.
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost]
        [ActionName(RouteConsts.Notify.ActiveChanged.Name)]
        public NDbResult ActiveChanged()
        {
            NDbResult result = new NDbResult();
            result.Success();
            TANofifyService.Instance.RaiseActiveTSBChanged();
            return result;
        }

        #endregion

        #region ShiftChanged

        /// <summary>
        /// Call when TSB Shift Changed. 
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost]
        [ActionName(RouteConsts.Notify.ShiftChanged.Name)]
        public NDbResult ShiftChanged()
        {
            NDbResult result = new NDbResult();
            result.Success();
            TANofifyService.Instance.RaiseChangeShift();
            return result;
        }

        #endregion
    }

    public class TANofifyService
    {
        #region Singelton

        private static TANofifyService _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static TANofifyService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(TANofifyService))
                    {
                        _instance = new TANofifyService();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TANofifyService() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TANofifyService()
        {
        }

        #endregion

        #region Public Methods

        public void RaiseActiveTSBChanged()
        {
            OnActiveTSBChanged.Call(this, EventArgs.Empty);
        }

        public void RaiseChangeShift()
        {
            OnChangeShift.Call(this, EventArgs.Empty);
        }

        #endregion

        #region Public Events

        public event EventHandler OnActiveTSBChanged;
        public event EventHandler OnChangeShift;

        #endregion
    }
}
