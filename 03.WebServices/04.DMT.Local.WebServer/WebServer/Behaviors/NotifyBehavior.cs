#region Using

using NLib;
using System;
using System.Reflection;
using System.Threading;

// web socket
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

#endregion

namespace DMT.Services.Behaviors
{
    public class NotifyBehavior : WebSocketBehavior
    {
        #region Internal Variables

        private static int _number = 0;

        #endregion

        #region Constructor and Destructor

        public NotifyBehavior() : base()
        {
            LocalDbServer.Instance.OnChangeShift += Instance_OnChangeShift;
            LocalDbServer.Instance.OnActiveTSBChanged += Instance_OnActiveTSBChanged;
        }

        ~NotifyBehavior()
        {
            LocalDbServer.Instance.OnActiveTSBChanged -= Instance_OnActiveTSBChanged;
            LocalDbServer.Instance.OnChangeShift -= Instance_OnChangeShift;
        }

        #endregion

        #region LocalDbServer event handers

        private void Instance_OnChangeShift(object sender, EventArgs e)
        {
            // send message to all clients.
            string message = NofifyConsts.ChangeShift;
            Send(message);
        }

        private void Instance_OnActiveTSBChanged(object sender, EventArgs e)
        {
            // send message to all clients.
            string message = NofifyConsts.ActiveTSBChanged;
            Send(message);
        }

        #endregion

        #region Override Methods

        protected override void OnOpen()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                Interlocked.Increment(ref _number);
                Sessions.Broadcast(string.Format("info:client={0} ", _number));
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                Interlocked.Decrement(ref _number);
                Sessions.Broadcast(string.Format("info:client={0} ", _number));
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                string msg = e.Data;
                Sessions.Broadcast(msg);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion
    }
}
