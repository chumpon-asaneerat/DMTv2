#region Using

using System;
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
        private static int _number = 0;

        protected override void OnOpen()
        {
            try
            {
                Interlocked.Increment(ref _number);
                Sessions.Broadcast(string.Format("info:client={0} ", _number));
            }
            catch (Exception) { }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            try
            {
                Interlocked.Decrement(ref _number);
                Sessions.Broadcast(string.Format("info:client={0} ", _number));
            }
            catch (Exception) { }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                string msg = e.Data;
                Sessions.Broadcast(msg);
            }
            catch (Exception) { }
        }
    }
}
