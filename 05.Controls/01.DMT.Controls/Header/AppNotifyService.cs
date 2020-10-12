#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLib;

#endregion

namespace DMT
{
    public class AppNofifyService
    {
        #region Singelton

        private static AppNofifyService _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static AppNofifyService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(AppNofifyService))
                    {
                        _instance = new AppNofifyService();
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
        private AppNofifyService() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~AppNofifyService()
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
