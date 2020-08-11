#region Using

using System;
using System.Collections.Generic;
using System.Linq;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    public class NError
    {
        public bool hasError { get; set; }
        public int errNum { get; set; }
        public string errMsg { get; set; }
    }

    public abstract class NResult
    {
        public NError errors { get; set; }

        public void DatabaseNotConnected()
        {
            this.errors = new NError();
            this.errors.errNum = -1;
            this.errors.errMsg = "No database connection.";
        }

        public void Success()
        {
            this.errors = new NError();
            this.errors.errNum = 0;
            this.errors.errMsg = "Success.";
        }

        public void Error(Exception ex)
        {
            this.errors = new NError();
            this.errors.errNum = -1;
            this.errors.errMsg = ex.Message;
        }
    }

    public class NResult<T> : NResult
        where T : class, new()
    {
        public T data { get; set; }
    }

    public class NResult<T, O> : NResult
        where T : class, new()
        where O : class, new()
    {
        public T data { get; set; }
        public O Out { get; set; }
    }
}
