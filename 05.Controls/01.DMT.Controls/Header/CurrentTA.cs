#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.Controls
{
    public static class TAApp
    {
        public static class User
        {
            public static Models.User Current { get; set; }
        }
    }
}
